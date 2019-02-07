using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator = DialogCoordinator.Instance;

        private Profile _currentProfile;
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

        private HamburgerMenuItemCollection _menuItems;
        public HamburgerMenuItemCollection MenuItems
        {
            get => _menuItems;
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                //OnPropertyChanged();
            }
        }

        private HamburgerMenuItemCollection _menuOptionItems;
        public HamburgerMenuItemCollection MenuOptionItems
        {
            get => _menuOptionItems;
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                //OnPropertyChanged();
            }
        }

        private ICommand _exitApplicationCommand;
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
                               AnimateHide = false
                           };

                           var dialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Confirm exit",
                               "Are you sure that you want to quit?",
                               MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
                           if (dialogResult != MessageDialogResult.Affirmative) return;

                           Application.Current.Shutdown();
                       }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CreateMenuItems();
            //TODO: save last used profile
            //ServerSettingsFactory
            if (CurrentProfile == null)
            {
                CurrentProfile = new Profile
                {
                    ServerSettings = new A3ServerSettings()
                };
            }
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
                    Tag = new GeneralViewModel(this)
                },

                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Account},
                    Label = "Profiles",
                    ToolTip = "Server profiles",
                    Tag = new ProfilesViewModel(ServiceLocator.Current.GetInstance<IDialogCoordinator>(), this)
                },

                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Settings},
                    Label = "Settings",
                    ToolTip = "Application settings",
                    Tag = new SettingsViewModel(this)
                }
            };

            MenuOptionItems = new HamburgerMenuItemCollection
            {
                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Help},
                    Label = "About",
                    ToolTip = "Some help.",
                    Tag = new AboutViewModel(this)
                }
            };
        }
    }
}