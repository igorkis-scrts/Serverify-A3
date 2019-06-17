using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using A3ServerTool.Models;
using A3ServerTool.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using Interchangeable.Enums;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class ProfilesViewModel : ViewModelBase
    {
        public const string Token = nameof(ProfilesViewModel);

        private readonly MainViewModel _mainViewModel;
        private readonly IProfileDirector _profileDirector;

        private readonly IDialogCoordinator _dialogCoordinator = DialogCoordinator.Instance;
        private readonly CustomDialog _customDialog = new CustomDialog(); //TODO: isn't it a little MVVM break?

        private bool _isRegistered;

        public ObservableCollection<Profile> Profiles
        {
            get => _profiles;
            set
            {
                if (Equals(_profiles, value))
                {
                    return;
                }
                _profiles = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<Profile> _profiles;

        public Profile SelectedProfile
        {
            get => _selectedProfile;
            set
            {
                if (Equals(_selectedProfile, value))
                {
                    return;
                }

                _selectedProfile = value;
                RaisePropertyChanged();
            }
        }
        private Profile _selectedProfile;

        public DialogResult<Profile> DialogResult
        {
            get => _dialogResult;
            set
            {
                if (Equals(_dialogResult, value))
                {
                    return;
                }
                _dialogResult = value;
                RaisePropertyChanged();
            }
        }
        private DialogResult<Profile> _dialogResult;

        public ProfilesViewModel(MainViewModel viewModel, IProfileDirector profileDirector)
        {
            _profileDirector = profileDirector;
            _mainViewModel = viewModel;

            Messenger.Default.Register<string>(this, MainViewModel.Token, DoByRequest);

            RefreshData();
        }

        //A tricky (and stupid too) way to beat the 
        //"Context is not registered. Consider using DialogParticipation.Register in XAML to bind in the DataContext" error
        //We can't register methods in constructor, so we should do it when userControl is rendered
        //https://stackoverflow.com/questions/41663538/trouble-with-showing-a-mahapps-metro-dialog-with-a-reactiveui-command
        //TODO: find better way to create listener only one time (right now it is constantly checking isRegistered field) - maybe different event exists?
        public ICommand StartupCommand
        {
            get
            {
                return new RelayCommand(_ =>
                {
                    if (_isRegistered) return;

                    Messenger.Default.Register<DialogResult<Profile>>(this, Token, ProcessMessage);
                    _isRegistered = true;
                });
            }
        }

        public ICommand SelectProfileCommand
        {
            get
            {
                return _selectProfileCommand ??
                       (_selectProfileCommand = new RelayCommand(_ =>
                       {
                           _mainViewModel.CurrentProfile = SelectedProfile;
                       }, _ => SelectedProfile != null));
            }
        }
        private ICommand _selectProfileCommand;

        public ICommand CreateProfileCommand
        {
            get
            {
                return _createProfileCommand ??
                       (_createProfileCommand = new RelayCommand(_ =>
                       {
                           ShowDialog();
                           //Messenger.Default.Send(_mainViewModel.CurrentProfile);
                           Messenger.Default.Send(ViewMode.New);
                       }));
            }
        }
        private ICommand _createProfileCommand;

        public ICommand DeleteProfileCommand
        {
            get
            {
                return _deleteProfileCommand ??
                       (_deleteProfileCommand = new RelayCommand(async _ =>
                       {
                           var dialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Confirm deletion",
                               "Are you sure that you want to delete this item?", MessageDialogStyle.AffirmativeAndNegative);
                           if (dialogResult != MessageDialogResult.Affirmative) return;

                           _profileDirector.Delete(SelectedProfile);
                           _mainViewModel.CurrentProfile = new Profile(Guid.NewGuid());

                           RefreshData();
                       }, _ => SelectedProfile != null));
            }
        }
        private ICommand _deleteProfileCommand;

        public ICommand EditProfileCommand
        {
            get
            {
                return _editProfileCommand ??
                       (_editProfileCommand = new RelayCommand(_ =>
                       {
                           ShowDialog();
                           Messenger.Default.Send(SelectedProfile);
                           Messenger.Default.Send(ViewMode.Edit);
                       }, _ => SelectedProfile != null));
            }
        }
        private ICommand _editProfileCommand;

        private void RefreshData()
        {
            Profiles = new ObservableCollection<Profile>(_profileDirector.GetAll());
        }

        private async void ShowDialog()
        {
            _customDialog.Content = new ProfileDialogView(); //MVVM break, obviously :(
            await _dialogCoordinator.ShowMetroDialogAsync(this, _customDialog);
        }

        private async void ProcessMessage(DialogResult<Profile> messageContent)
        {
            DialogResult = messageContent;
            await _dialogCoordinator.HideMetroDialogAsync(this, _customDialog);

            if (DialogResult.Message == MessageDialogResult.Affirmative)
            {
                _profileDirector.SetDefaultValues(DialogResult.Object);
                _profileDirector.SaveStorage(DialogResult.Object);

                if (Equals(DialogResult.Object.Id, _mainViewModel.CurrentProfile?.Id))
                {
                    _mainViewModel.CurrentProfile = DialogResult.Object;
                }
            }

            RefreshData();
            ClearDialogServices();
        }

        /// <summary>
        /// Clears view models when we don't need them
        /// </summary>
        /// <remarks>Since ServiceLocator creates new instance of ProfileDialogVM every time we're calling it,
        /// we need to manualy unregister that VMs after it's usage so there will be no memory leak</remarks>
        private void ClearDialogServices()
        {
            Task.Run(() =>
            {
                var servicesToClear = ServiceLocator.Current.GetAllInstances<ProfileDialogViewModel>()
                    .Where(service => service.IsExpired).ToList();

                foreach (var service in servicesToClear)
                {
                    SimpleIoc.Default.Unregister(service);
                }
            });
            //TODO: catch exceptions
        }

        //TODO: change string to enum
        /// <summary>
        /// Updates datagrid by requests from other view models.
        /// </summary>
        /// <param name="request">message to do something in this viewmodel.</param>
        private void DoByRequest(string request)
        {
            RefreshData();
        }
    }
}
