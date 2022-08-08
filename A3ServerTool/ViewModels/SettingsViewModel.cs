namespace A3ServerTool.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly IThemeParser _themeParser;
    
    public List<CultureInfo> Cultures => App.Languages;

    public CultureInfo Culture
    {
        get => _culture;
        set
        {
            if (Equals(value, _culture))
            {
                return;
            }

            _culture = value;
            RaisePropertyChanged();
        }
    }
    private CultureInfo _culture;

    /// <summary>
    /// Gets or sets the background theme.
    /// </summary>
    public ThemeType Theme
    {
        get => _theme;
        set
        {
            if (Equals(value, _theme))
            {
                return;
            }

            _theme = value;
            RaisePropertyChanged();
        }
    }
    private ThemeType _theme;

    /// <summary>
    /// Gets the save profile command.
    /// </summary>
    public ICommand SaveSettingsCommand
    {
        get
        {
            return _saveSettingsCommand ??= new RelayCommand(_ =>
            {
                ApplyAndSaveSettings();
            });
        }
    }
    private ICommand _saveSettingsCommand;

    public SettingsViewModel(IThemeParser themeParser)
    {
        _themeParser = themeParser;
        
        Culture = Properties.Settings.Default.Language;
        Theme = _themeParser.ConvertRealTitleToTheme(Properties.Settings.Default.Theme);
    }

    /// <summary>
    /// Saves the language settings.
    /// </summary>
    private void ApplyAndSaveSettings()
    {
        App.Language = Culture;
        App.Theme = _themeParser.ConvertThemeToRealTitle(Theme);
    }
}
