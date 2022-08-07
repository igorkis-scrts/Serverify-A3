namespace A3ServerTool.ViewModels.ServerSubViewModels;

/// <summary>
/// Represents view model with logging server properties.
/// </summary>
/// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
/// <seealso cref="System.ComponentModel.IDataErrorInfo" />
public class LoggingViewModel : ViewModelBase, IDataErrorInfo
{
    private readonly ServerViewModel _parentViewModel;

    private Profile CurrentProfile => _parentViewModel.CurrentProfile;

    /// <summary>
    /// Gets or sets the call extension report limit.
    /// </summary>
    public float? CallExtensionReportLimit
    {
        get => CurrentProfile?.ServerConfig.CallExtensionReportLimit;
        set
        {
            if (Equals(value, CurrentProfile?.ServerConfig.CallExtensionReportLimit)) return;
            if (CurrentProfile?.ServerConfig == null) return;
            
            CurrentProfile.ServerConfig.CallExtensionReportLimit = value;
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the console log file name.
    /// </summary>
    public string LogFileName
    {
        get => CurrentProfile?.ServerConfig?.LogFileName;
        set
        {
            if (Equals(value, CurrentProfile.ServerConfig?.LogFileName)) return;
            if (CurrentProfile?.ServerConfig == null) return;
            
            CurrentProfile.ServerConfig.LogFileName = !value.Contains(".log")
                ? value + ".log"
                : value;
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the automatic mission test path.
    /// </summary>
    public string AutoTestPath
    {
        get => CurrentProfile?.ArgumentSettings?.AutoTestPath;
        set
        {
            if (Equals(value, CurrentProfile?.ArgumentSettings?.AutoTestPath)) return;
            if (CurrentProfile?.ArgumentSettings == null) return;
            
            CurrentProfile.ArgumentSettings.AutoTestPath = value;
            Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [are logs disabled].
    /// </summary>
    public bool AreLogsDisabled
    {
        get => CurrentProfile == null || CurrentProfile.ArgumentSettings == null
            ? false
            : CurrentProfile.ArgumentSettings.AreLogsDisabled;
        set
        {
            if (Equals(value, CurrentProfile.ArgumentSettings.AreLogsDisabled)) return;
            CurrentProfile.ArgumentSettings.AreLogsDisabled = value;
            Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is traffic logged.
    /// </summary>
    public bool IsTrafficLogged
    {
        get => CurrentProfile == null || CurrentProfile.ArgumentSettings == null
            ? false
            : CurrentProfile.ArgumentSettings.IsTrafficLogged;
        set
        {
            if (Equals(value, CurrentProfile.ArgumentSettings.IsTrafficLogged)) return;
            CurrentProfile.ArgumentSettings.IsTrafficLogged = value;
            Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the time stamp format.
    /// </summary>
    public TimeStampType TimeStampFormat
    {
        get
        {
            Enum.TryParse(CurrentProfile.ServerConfig.TimeStampFormat.FirstLetterToUpperCase(), out TimeStampType timeStampFormat);
            return timeStampFormat;
        }
        set
        {
            if (Equals(value.ToString(), CurrentProfile.ServerConfig.TimeStampFormat)) return;
            CurrentProfile.ServerConfig.TimeStampFormat = value.ToString().ToLower();
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// Gets the browse command.
    /// </summary>
    public ICommand BrowseCommand
    {
        get
        {
            return _browseCommand ??= new RelayCommand(_ =>
            {
                using var fileDialog = new OpenFileDialog();
                
                if (fileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                AutoTestPath = fileDialog.FileName;
            });
        }
    }
    private ICommand _browseCommand;

    public LoggingViewModel(ServerViewModel viewModel)
    {
        _parentViewModel = viewModel;
    }

    /// <summary>
    /// Opens the hyperlink in browser.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RequestNavigateEventArgs"/> instance containing the event data.</param>
    public void OpenHyperlinkInBrowser(object sender, RequestNavigateEventArgs e)
    {
        UriDirector.OpenUri(e.Uri.AbsoluteUri);
        e.Handled = true;
    }

    public string this[string columnName] => string.Empty;

    public string Error => string.Empty;
}
