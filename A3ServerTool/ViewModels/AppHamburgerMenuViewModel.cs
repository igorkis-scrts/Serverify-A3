namespace A3ServerTool.ViewModels;

public class AppHamburgerMenuViewModel : ViewModelBase
{
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
    
    public ICommand WindowLoadedCommand
    {
        get
        {
            return _windowLoadedCommand ??= new RelayCommand(_ =>
            {
                CreateMenuItems();
            });
        }
    }
    private ICommand _windowLoadedCommand;
    
    private void CreateMenuItems()
    {
        MenuItems = new HamburgerMenuItemCollection
        {
            new HamburgerMenuIconItem
            {
                Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Server},
                Label = Properties.StaticLang.ServerHamburgerMenuLocalizedTitle,
                ToolTip = Properties.StaticLang.ServerHamburgerMenuLocalizedTooltip,
                Tag = SimpleIoc.Default.GetInstance<ServerViewModel>()
            },

            new HamburgerMenuIconItem
            {
                Icon = new PackIconMaterial {Kind = PackIconMaterialKind.AccountMultiple},
                Label = Properties.StaticLang.ProfilesHamburgerMenuLocalizedTitle,
                ToolTip = Properties.StaticLang.ProfilesHamburgerMenuLocalizedTooltip,
                Tag =  SimpleIoc.Default.GetInstance<ProfilesViewModel>()
            },

            new HamburgerMenuIconItem
            {
                Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Cog},
                Label = Properties.StaticLang.SettingsHamburgerMenuLozalizedTitle,
                ToolTip = Properties.StaticLang.SettingsHamburgerMenuLozalizedTooltip,
                Tag = SimpleIoc.Default.GetInstance<SettingsViewModel>()
            }
        };

        MenuOptionItems = new HamburgerMenuItemCollection
        {
            new HamburgerMenuIconItem
            {
                Icon = new PackIconMaterial {Kind = PackIconMaterialKind.Help},
                Label = Properties.StaticLang.AboutHamburgerMenuLozalizedTitle,
                ToolTip = Properties.StaticLang.AboutHamburgerMenuLozalizedTooltip,
                Tag = SimpleIoc.Default.GetInstance<AboutViewModel>()
            }
        };
    }
}
