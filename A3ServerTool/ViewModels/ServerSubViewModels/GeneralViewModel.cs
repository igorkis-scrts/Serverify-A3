using A3ServerTool.Enums;
using A3ServerTool.Helpers.ServerLauncher;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using A3ServerTool.Models.Profile;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents common server properties.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class GeneralViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;
        private readonly IServerStringBuilder _serverStringBuilder;

        public const string ExecutablePathToken = "ExecutablePathToken";
        public const string RankingPathToken = "RankingPathToken";
        public const string Token = nameof(GeneralViewModel);

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets the final server argument string.
        /// </summary>
        public string FinalServerArgumentString => _serverStringBuilder != null && CurrentProfile != null
            ? _serverStringBuilder.FinalArgumentString
            : string.Empty;


        /// <summary>
        /// Gets or sets server name visible in the game browser.
        /// </summary>
        public string Name
        {
            get => CurrentProfile?.ServerConfig?.HostName;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.HostName)) return;
                CurrentProfile.ServerConfig.HostName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the process identifier file.
        /// </summary>
        public string ProcessIdFileName
        {
            get => CurrentProfile?.ArgumentSettings?.ProcessIdFileName;
            set
            {
                if (Equals(value, CurrentProfile?.ArgumentSettings?.ProcessIdFileName)) return;
                CurrentProfile.ArgumentSettings.ProcessIdFileName = value;

                RaisePropertyChanged("FinalServerArgumentString");
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ranking file path.
        /// </summary>
        public string RankingFilePath
        {
            get => CurrentProfile?.ArgumentSettings?.RankingFilePath;
            set
            {
                if (Equals(value, CurrentProfile?.ArgumentSettings?.RankingFilePath)) return;
                CurrentProfile.ArgumentSettings.RankingFilePath = value;
                RaisePropertyChanged("FinalServerArgumentString");
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required to connect to server. 
        /// </summary>
        public string ServerPassword
        {
            get => CurrentProfile?.ServerConfig?.Password;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.Password)) return;
                CurrentProfile.ServerConfig.Password = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required to connect to server. 
        /// </summary>
        public int? MaximumAmountOfPlayers
        {
            get => CurrentProfile?.ServerConfig?.MaximumAmountOfPlayers;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.MaximumAmountOfPlayers)) return;
                CurrentProfile.ServerConfig.MaximumAmountOfPlayers = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required to connect to server. 
        /// </summary>
        public int? HeadlessClients
        {
            get => CurrentProfile?.HeadlessClients;
            set
            {
                if (Equals(value, CurrentProfile?.HeadlessClients)) return;
                CurrentProfile.HeadlessClients = value;
                RaisePropertyChanged();
            }
        }

        public string ExecutablePath
        {
            get => CurrentProfile?.ExecutablePath;
            set
            {
                if (Equals(value, CurrentProfile?.ExecutablePath)) return;
                CurrentProfile.ExecutablePath = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the welcome messages.
        /// </summary>
        public string WelcomeMessages
        {
            get
            {
                var messages = CurrentProfile?.ServerConfig.WelcomeMessages;
                if (messages != null)
                {
                    return string.Join("\n", messages);
                }
                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.WelcomeMessages = null;
                }
                else
                {
                    var valueAsArray = value?.Split('\n');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.WelcomeMessages)) return;
                    CurrentProfile.ServerConfig.WelcomeMessages = valueAsArray;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the interval between welcome messages.
        /// </summary>
        public int? IntervalBetweenWelcomeMessages
        {
            get => CurrentProfile?.ServerConfig?.IntervalBetweenWelcomeMessages;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.IntervalBetweenWelcomeMessages)) return;
                CurrentProfile.ServerConfig.IntervalBetweenWelcomeMessages = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the headless client IP adresses.
        /// </summary>
        public string HeadlessClientIps
        {
            get
            {
                var ips = CurrentProfile?.ServerConfig?.HeadlessClientIps;
                if (ips != null)
                {
                    return string.Join(",", ips);
                }
                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.HeadlessClientIps = null;
                }
                else
                {
                    if (value != null)
                    {
                        value = Regex.Replace(value, @"\s+", "");
                    }
                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig?.HeadlessClientIps)) return;
                    CurrentProfile.ServerConfig.HeadlessClientIps = valueAsArray.ToList();
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the local client IP adresses.
        /// </summary>
        public string LocalClientIps
        {
            get
            {
                var ips = CurrentProfile?.ServerConfig?.LocalClientIps;
                if (ips != null)
                {
                    return string.Join(",", ips);
                }
                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.LocalClientIps = null;
                }
                else
                {
                    if (value != null)
                    {
                        value = Regex.Replace(value, @"\s+", "");
                    }
                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig?.LocalClientIps)) return;
                    CurrentProfile.ServerConfig.LocalClientIps = valueAsArray.ToList();
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the voting treshold.
        /// </summary>
        public float? VotingTreshold
        {
            get => CurrentProfile?.ServerConfig?.VoteThreshold;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.VoteThreshold)) return;
                CurrentProfile.ServerConfig.VoteThreshold = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number of players needed to start mission voting.
        /// </summary>
        public int? VotingStartMission
        {
            get => CurrentProfile?.ServerConfig.VoteMissionPlayers;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.VoteMissionPlayers)) return;
                CurrentProfile.ServerConfig.VoteMissionPlayers = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the voting timeout.
        /// </summary>
        public int? VotingTimeout
        {
            get => CurrentProfile?.ServerConfig?.VotingTimeout;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.VotingTimeout)) return;
                CurrentProfile.ServerConfig.VotingTimeout = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the role selection timeout.
        /// </summary>
        public int? RoleSelectionTimeout
        {
            get => CurrentProfile?.ServerConfig.RoleSelectionTimeout;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.RoleSelectionTimeout)) return;
                CurrentProfile.ServerConfig.RoleSelectionTimeout = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the briefing timeout.
        /// </summary>
        public int? BriefingTimeout
        {
            get => CurrentProfile?.ServerConfig?.BriefingTimeout;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.BriefingTimeout)) return;
                CurrentProfile.ServerConfig.BriefingTimeout = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the debriefing timeout.
        /// </summary>
        public int? DebriefingTimeout
        {
            get => CurrentProfile?.ServerConfig?.DebriefingTimeout;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.DebriefingTimeout)) return;
                CurrentProfile.ServerConfig.DebriefingTimeout = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the debriefing timeout.
        /// </summary>
        public int? LobbyIdleTimeout
        {
            get => CurrentProfile?.ServerConfig?.LobbyIdleTimeout;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.LobbyIdleTimeout)) return;
                CurrentProfile.ServerConfig.LobbyIdleTimeout = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is drawing on map allowed.
        /// </summary>
        public bool IsDrawingOnMapAllowed
        {
            get => CurrentProfile == null || CurrentProfile.ServerConfig == null
                ? false
                : CurrentProfile.ServerConfig.IsDrawingOnMapAllowed;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.IsDrawingOnMapAllowed)) return;
                CurrentProfile.ServerConfig.IsDrawingOnMapAllowed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is mission automatic initialized.
        /// </summary>
        public bool IsMissionAutoInitialized
        {
            get => CurrentProfile == null || CurrentProfile.ArgumentSettings == null
                ? false
                : CurrentProfile.ArgumentSettings.IsMissionAutoInitialized;
            set
            {
                if (Equals(value, CurrentProfile?.ArgumentSettings?.IsMissionAutoInitialized)) return;
                CurrentProfile.ArgumentSettings.IsMissionAutoInitialized = value;
                RaisePropertyChanged("FinalServerArgumentString");
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is VON messaging enabled.
        /// </summary>
        /// <value>
        /// The is von enabled.
        /// </value>
        public int? IsVonDisabled
        {
            get => CurrentProfile?.ServerConfig.DisableVoice;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.DisableVoice)) return;
                CurrentProfile.ServerConfig.DisableVoice = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the is VON messaging enabled.
        /// </summary>
        /// <value>
        /// The is von enabled.
        /// </value>
        public int? VonCodecQuality
        {
            get => CurrentProfile?.ServerConfig.VoiceCodecQuality;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.VoiceCodecQuality)) return;
                CurrentProfile.ServerConfig.VoiceCodecQuality = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the voice codec.
        /// </summary>
        public VoiceCodecType VoiceCodecType
        {
            get
            {
                return CurrentProfile?.ServerConfig?.VoiceCodecType != null
                    ? (VoiceCodecType)CurrentProfile.ServerConfig.VoiceCodecType
                    : VoiceCodecType.Opus;
            }
            set
            {
                if (Equals((int)value, CurrentProfile.ServerConfig.VoiceCodecType)) return;
                CurrentProfile.ServerConfig.VoiceCodecType = (int)value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the value that checks if server is persistent.
        /// </summary>
        public int? IsPersistent
        {
            get => CurrentProfile?.ServerConfig?.IsPersistent;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.IsPersistent)) return;
                CurrentProfile.ServerConfig.IsPersistent = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets value that represents if server collects analytics.
        /// </summary>
        public int? HasAnalytics
        {
            get => CurrentProfile?.ServerConfig.HasBisAnalytics;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.HasBisAnalytics)) return;
                CurrentProfile.ServerConfig.HasBisAnalytics = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the type of the rotor library flight model.
        /// </summary>
        public RotorLibType RotorLibType
        {
            get
            {
                return CurrentProfile?.ServerConfig?.RotorLibSimulationType != null
                    ? (RotorLibType)CurrentProfile.ServerConfig.RotorLibSimulationType
                    : RotorLibType.Default;
            }
            set
            {
                if (Equals((int)value, CurrentProfile?.ServerConfig?.RotorLibSimulationType)) return;
                CurrentProfile.ServerConfig.RotorLibSimulationType = (int)value;
                RaisePropertyChanged();
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ??
                       (_browseCommand = new RelayCommand(callerToken =>
                       {
                           using (var fileDialog = new OpenFileDialog())
                           {
                               var callerTokenString = callerToken.ToString();
                               if (callerTokenString == ExecutablePathToken)
                               {
                                   fileDialog.Filter = "Exe files (*.exe) | *.exe;";
                               }
                               else if (callerTokenString == RankingPathToken)
                               {
                                   fileDialog.Filter = "log files (*.log)|*.log;";
                               }

                               if (fileDialog.ShowDialog() != DialogResult.OK)
                               {
                                   return;
                               }

                               if (callerTokenString == ExecutablePathToken)
                               {
                                   ExecutablePath = fileDialog.FileName;
                               }
                               else if(callerTokenString == RankingPathToken)
                               {
                                   RankingFilePath = fileDialog.FileName;
                               }
                           }
                       }));
            }
        }
        private ICommand _browseCommand;

        public GeneralViewModel(ServerViewModel viewModel, IServerStringBuilder serverStringBuilder)
        {
            _parentViewModel = viewModel;
            _serverStringBuilder = serverStringBuilder;
            _serverStringBuilder.Profile = CurrentProfile;
            Messenger.Default.Register<string>(this, Token, UpdateFinalStringByRequest);
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

        /// <summary>
        /// Updates the final string by requests from Message Bus.
        /// </summary>
        private void UpdateFinalStringByRequest(string message)
        {
            if(_serverStringBuilder.Profile != CurrentProfile)
            {
                _serverStringBuilder.Profile = CurrentProfile;
            }
            RaisePropertyChanged("FinalServerArgumentString");
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name) when string.IsNullOrWhiteSpace(Name):
                        return "Name can't be null or empty.";
                    case nameof(ExecutablePath) when string.IsNullOrWhiteSpace(ExecutablePath):
                        return "Server path can't be null or empty.";
                    case nameof(HeadlessClientIps) when !string.IsNullOrWhiteSpace(HeadlessClientIps)
                            && Regex.Matches(HeadlessClientIps, "[a-zA-Z]").Count > 0:
                    case nameof(LocalClientIps) when !string.IsNullOrWhiteSpace(LocalClientIps)
                            && Regex.Matches(LocalClientIps, "[a-zA-Z]").Count > 0:
                        return "IPs can be only numbers.";

                    default:
                        return null;
                }
            }
        }

        public string Error
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return " Name of the server must be specified.";
                }
                else if (string.IsNullOrWhiteSpace(ExecutablePath))
                {
                    return "Server path must be specified.";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}
