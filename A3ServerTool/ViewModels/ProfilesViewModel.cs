using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using A3ServerTool.Models;
using A3ServerTool.ProfileStorage;
using A3ServerTool.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class ProfilesViewModel : PropertyChangedViewModel
    {
        private readonly MainViewModel _mainViewModel;

        private readonly IDialogCoordinator _dialogCoordinator; 
        private readonly CustomDialog _customDialog = new CustomDialog(); //TODO: isn't it a little MVVM break?

        private bool _isRegistered;

        private IProfileDao _profileDao;
        public IProfileDao ProfileDao
        {
            get
            {
                if (_profileDao != null) return _profileDao;

                _profileDao = ProfileDaoFactory.GetDao(DaoType.Json);
                return _profileDao;
            }
        }

        private ObservableCollection<Profile> _profiles;
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
                OnPropertyChanged();
            }

        }

        private Profile _selectedProfile;
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
                OnPropertyChanged();
            }

        }

        private DialogResult<Profile> _dialogResult;
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
                OnPropertyChanged();
            }
        }


        public ProfilesViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            Profiles = ProfileDao.GetAll();
        }

        //A tricky (and stupid too) way to beat the 
        //"Context is not registered. Consider using DialogParticipation.Register in XAML to bind in the DataContext" error
        //We can't register methods in constructor, so we should do it when userControl is rendered
        //https://stackoverflow.com/questions/41663538/trouble-with-showing-a-mahapps-metro-dialog-with-a-reactiveui-command
        //TODO: better way to create listener only one time (right now it is constantly checking isRegistered field) - maybe different event exists?
        public ICommand StartupCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (_isRegistered) return;

                    Messenger.Default.Register<DialogResult<Profile>>(this, ProcessMessage);
                    _isRegistered = true;
                });
            }
        }

        private ICommand _createProfileCommand;
        public ICommand CreateProfileCommand
        {
            get
            {
                return _createProfileCommand ??
                       (_createProfileCommand = new RelayCommand(obj =>
                       {
                           ShowDialog();
                       }));
            }
        }

        private ICommand _deleteProfileCommand;
        public ICommand DeleteProfileCommand
        {
            get
            {
                return _deleteProfileCommand ??
                       (_deleteProfileCommand = new RelayCommand(async obj =>
                       {
                           var profileToDelete = SelectedProfile;
                           if (profileToDelete == null) return;


                           var dialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Confirm deletion",
                               "Are you sure that you want to delete this item?", MessageDialogStyle.AffirmativeAndNegative);
                           if (dialogResult != MessageDialogResult.Affirmative) return;

                           ProfileDao.Delete(profileToDelete);
                           RefreshData();
                       }));
            }
        }

        private ICommand _editProfileCommand;
        public ICommand EditProfileCommand
        {
            get
            {
                return _editProfileCommand ??
                       (_editProfileCommand = new RelayCommand(async obj =>
                       {
                           var profileToEdit = SelectedProfile;
                           if (profileToEdit == null) return;

                           ShowDialog();
                           Messenger.Default.Send(SelectedProfile);

                           //TODO: Save to HDD, storage

                           //RefreshData();
                       }));
            }
        }

        private void RefreshData()
        {
            Profiles = ProfileDao.GetAll();
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

            if (DialogResult.MessageResult == MessageDialogResult.Affirmative)
            {
                ProfileDao.Insert(DialogResult.ObjectResult);
                Profiles.Add(DialogResult.ObjectResult);
                //TODO:asynchrously (or task/thread usage) save object to local storage
            }

            ClearDialogServicesAsync();
        }

        /// <summary>
        /// Clears view models when we don't need 'em
        /// </summary>
        /// <remarks>Since ServiceLocator creates new instance of ProfileDialogVM every time we're calling it,
        /// we need to manualy unregister that VMs after it's usage so there will be no memory leak</remarks>
        private async void ClearDialogServicesAsync()
        {
            await Task.Run(() =>
            {
                var servicesToClear = ServiceLocator.Current.GetAllInstances<ProfileDialogViewModel>()
                    .Where(service => service.IsExpired).ToList();

                foreach (var service in servicesToClear)
                {
                    SimpleIoc.Default.Unregister(service);
                }
            });
        }
    }
}
