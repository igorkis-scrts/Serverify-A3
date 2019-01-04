using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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
        private HamburgerMenuItemCollection _menuItems;

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
        private HamburgerMenuItemCollection _menuOptionItems;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CreateMenuItems();
        }

        private void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection
            {
                //new HamburgerMenuIconItem
                //{
                //    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Server},
                //    Label = "Server",
                //    ToolTip = "General game server tuning",
                //    Tag = new GeneralViewModel(this)
                //},

                new HamburgerMenuIconItem
                {
                    Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Account},
                    Label = "Profiles",
                    ToolTip = "Server profiles",
                    Tag = new ProfilesViewModel(ServiceLocator.Current.GetInstance<IDialogCoordinator>())
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