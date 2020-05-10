using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.ViewModels.ServerSubViewModels;
using A3ServerTool.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using Interchangeable.Enums;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.ViewModels
{
    public class ProfilesViewModel : ViewModelBase
    {
        public const string Token = nameof(ProfilesViewModel);

        private readonly MainViewModel _mainViewModel;
        private readonly IProfileDirector _profileDirector;

        private readonly IDialogCoordinator _dialogCoordinator = DialogCoordinator.Instance;
        private readonly CustomDialog _customDialog = new CustomDialog(); //TODO: isn't it a little MVVM break?

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

        public SaveDialogResult<Profile> DialogResult
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
        private SaveDialogResult<Profile> _dialogResult;

        public ProfilesViewModel(MainViewModel viewModel, IProfileDirector profileDirector)
        {
            _profileDirector = profileDirector;
            _mainViewModel = viewModel;

            Messenger.Default.Register<string>(this, MainViewModel.Token, DoByRequest);
            Messenger.Default.Register<SaveDialogResult<Profile>>(this, Token, ProcessMessage);

            RefreshData();
        }

        public ICommand SelectProfileCommand
        {
            get
            {
                return _selectProfileCommand ??
                       (_selectProfileCommand = new RelayCommand(_ =>
                       {
                           if(Equals(_mainViewModel.CurrentProfile, SelectedProfile.Id))
                           {
                               return;
                           }

                           _mainViewModel.CurrentProfile = _profileDirector.GetById(SelectedProfile.Id);
                           Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
                           Messenger.Default.Send("UpdateMissions", MissionsViewModel.Token);
                           Messenger.Default.Send("UpdateMods", ModificationsViewModel.Token);
                       }, _ => SelectedProfile != null));
            }
        }
        private ICommand _selectProfileCommand;

        public ICommand SaveCurrentProfileCommand
        {
            get
            {
                return _saveCurrentProfileCommand ??
                       (_saveCurrentProfileCommand = new RelayCommand(async _ =>
                       {
                           if (string.IsNullOrEmpty(_mainViewModel.CurrentProfile.Name))
                           {
                               ShowDialog();
                               Messenger.Default.Send(_mainViewModel.CurrentProfile);
                               Messenger.Default.Send(ViewMode.Edit);
                           }
                           else
                           {
                               _profileDirector.SaveStorage(_mainViewModel.CurrentProfile);
                               Properties.Settings.Default.LastUsedProfile = _mainViewModel.CurrentProfile.Id;
                               Properties.Settings.Default.Save();

                               var dialogSettings = new MetroDialogSettings
                               {
                                   AffirmativeButtonText = "OK",
                                   ColorScheme = MetroDialogColorScheme.Accented
                               };

                               await ((MetroWindow)Application.Current.MainWindow)
                                   .ShowMessageAsync(Properties.StaticLang.SuccessTitle, Properties.StaticLang.SuccessfulSavedProfileText, MessageDialogStyle.Affirmative, dialogSettings);
                           }
                           RefreshData();
                       }, _ => _mainViewModel.CurrentProfile != null));
            }
        }
        private ICommand _saveCurrentProfileCommand;

        public ICommand CreateProfileCommand
        {
            get
            {
                return _createProfileCommand ??
                       (_createProfileCommand = new RelayCommand(_ =>
                       {
                           ShowDialog();
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
                           if (_mainViewModel.CurrentProfile.Id == SelectedProfile.Id)
                           {
                               _mainViewModel.CurrentProfile = new Profile(Guid.NewGuid());
                               Messenger.Default.Send("UpdateMissions", MissionsViewModel.Token);
                               Messenger.Default.Send("UpdateMods", ModificationsViewModel.Token);
                               Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
                           }

                           _profileDirector.Delete(SelectedProfile);

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

        /// <summary>
        /// Gets the view model loaded command.
        /// </summary>
        public ICommand ViewModelLoadedCommand
        {
            get
            {
                return _viewModelLoadedCommand ??
                       (_viewModelLoadedCommand = new RelayCommand(_ => RefreshData()));
            }
        }
        private ICommand _viewModelLoadedCommand;

        private void RefreshData()
        {
            Profiles = new ObservableCollection<Profile>(_profileDirector.GetAll());
        }

        private async void ShowDialog()
        {
            _customDialog.Content = new ProfileDialogView(); //MVVM break, obviously :(
            await _dialogCoordinator.ShowMetroDialogAsync(this, _customDialog);
        }

        private async void ProcessMessage(SaveDialogResult<Profile> messageContent)
        {
            DialogResult = messageContent;
            await _dialogCoordinator.HideMetroDialogAsync(this, _customDialog);

            if (DialogResult.Message == MessageDialogResult.Affirmative)
            {
                if(DialogResult.ActionType == SaveObjectActionType.Create)
                {
                    _profileDirector.SetDefaultValues(DialogResult.Object);
                }

                _mainViewModel.CurrentProfile = DialogResult.Object;
                _profileDirector.SaveStorage(DialogResult.Object);
                Properties.Settings.Default.LastUsedProfile = DialogResult.Object.Id;
                Properties.Settings.Default.Save();

                Messenger.Default.Send("UpdateMissions", MissionsViewModel.Token);
                Messenger.Default.Send("UpdateMods", ModificationsViewModel.Token);
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
                var servicesToClear = SimpleIoc.Default.GetAllInstances<ProfileDialogViewModel>()
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
