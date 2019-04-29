using System;
using System.Windows;
using System.Windows.Input;
using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public const string Token = nameof(MainViewModel);

        private readonly IDialogCoordinator _dialogCoordinator = DialogCoordinator.Instance;
        private readonly CustomDialog _customDialog = new CustomDialog();
        private readonly IProfileDirector _profileDirector;

        public Profile CurrentProfile
        {
            get => _currentProfile;
            set
            {
                //if (Equals(value, _currentProfile)) return;
                _currentProfile = value;
                RaisePropertyChanged();
            }
        }
        private Profile _currentProfile;

        public HamburgerMenuItemCollection MenuItems
        {
            get => _menuItems;
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                RaisePropertyChanged();
            }
        }
        private HamburgerMenuItemCollection _menuItems;

        public HamburgerMenuItemCollection MenuOptionItems
        {
            get => _menuOptionItems;
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                RaisePropertyChanged();
            }
        }
        private HamburgerMenuItemCollection _menuOptionItems;

        public ICommand ExitApplicationCommand
        {
            get
            {
                return _exitApplicationCommand ??
                       (_exitApplicationCommand = new RelayCommand(async obj =>
                       {
                           var dialogSettings = new MetroDialogSettings()
                           {
                               AffirmativeButtonText = "Quit",
                               NegativeButtonText = "Cancel",
                               AnimateShow = true,
                               AnimateHide = true
                           };

                           var dialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Confirm exit",
                               "Are you sure that you want to quit?",
                               MessageDialogStyle.AffirmativeAndNegative, dialogSettings);

                           if (dialogResult != MessageDialogResult.Affirmative) return;
                           Application.Current.Shutdown();
                       }));
            }
        }
        private ICommand _exitApplicationCommand;

        public ICommand WindowLoadedCommand
        {
            get
            {
                return _windowLoadedCommand ??
                       (_windowLoadedCommand = new RelayCommand(_ =>
                       {
                           CreateMenuItems();
                           Messenger.Default.Register<DialogResult<Profile>>(this, Token, ProcessMessageResult);

                           var lastProfileId = (Guid) SettingsCoordinator.Retrieve(ApplicationSettingType.LastUsedProfile);
                           if(lastProfileId != Guid.Empty)
                           {
                               CurrentProfile = _profileDirector.GetById(lastProfileId);
                           }
                           else
                           {
                               CurrentProfile = new Profile(Guid.NewGuid());
                           }
                       }));
            }
        }
        private ICommand _windowLoadedCommand;

        public ICommand SaveProfileCommand
        {
            get
            {
                return _saveProfileCommand ??
                       (_saveProfileCommand = new RelayCommand(async _ =>
                       {
                           if (_profileDirector.ExistOnStorage(CurrentProfile))
                           {
                               _profileDirector.SaveStorage(CurrentProfile);

                               var dialogSettings = new MetroDialogSettings
                               {
                                   AffirmativeButtonText = "OK",
                                   ColorScheme = MetroDialogColorScheme.Accented
                               };

                               await ((MetroWindow)Application.Current.MainWindow)
                                   .ShowMessageAsync("Success", "Profile was saved.", MessageDialogStyle.Affirmative, dialogSettings);
                           }
                           else
                           {
                               _customDialog.Content = new ProfileDialogView();
                               await _dialogCoordinator.ShowMetroDialogAsync(this, _customDialog);
                               Messenger.Default.Send(CurrentProfile);
                               Messenger.Default.Send(ViewMode.Save);
                           }
                       }));
            }
        }
        private ICommand _saveProfileCommand;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IProfileDirector director)
        {
            _profileDirector = director;
        }

        private void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Server},
                    Label = "Server",
                    ToolTip = "General game server tuning",
                    Tag = ServiceLocator.Current.GetInstance<GeneralViewModel>()
                },

                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Account},
                    Label = "Profiles",
                    ToolTip = "Server profiles",
                    Tag = ServiceLocator.Current.GetInstance<ProfilesViewModel>()
                },

                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Settings},
                    Label = "Settings",
                    ToolTip = "Application settings",
                    Tag = ServiceLocator.Current.GetInstance<SettingsViewModel>()
                }
            };

            MenuOptionItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Help},
                    Label = "About",
                    ToolTip = "Some help.",
                    Tag = ServiceLocator.Current.GetInstance<AboutViewModel>()
                }
            };
        }

        private async void ProcessMessageResult(DialogResult<Profile> messageContent)
        {
            var dialogResult = messageContent;
            await _dialogCoordinator.HideMetroDialogAsync(this, _customDialog);

            if (dialogResult.Message == MessageDialogResult.Affirmative)
            {
                _profileDirector.SaveStorage(dialogResult.Object);
                CurrentProfile = dialogResult.Object;
                SettingsCoordinator.Save(ApplicationSettingType.LastUsedProfile, CurrentProfile.Id);

                Messenger.Default.Send("update", Token);
            }
        }
    }
}