using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

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

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public string Port
        {
            get => CurrentProfile?.ArgumentSettings?.Port;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.Port)) return;
                CurrentProfile.ArgumentSettings.Port = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets server name visible in the game browser.
        /// </summary>
        public string Name
        {
            get => CurrentProfile?.ServerConfig.HostName;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.HostName)) return;
                CurrentProfile.ServerConfig.HostName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required to connect to server. 
        /// </summary>
        public string ServerPassword
        {
            get => CurrentProfile?.ServerConfig.Password;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.Password)) return;
                CurrentProfile.ServerConfig.Password = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required to connect to server. 
        /// </summary>
        public int? MaximumAmountOfPlayers
        {
            get => CurrentProfile?.ServerConfig.MaximumAmountOfPlayers;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.MaximumAmountOfPlayers)) return;
                CurrentProfile.ServerConfig.MaximumAmountOfPlayers = value;
                RaisePropertyChanged();
            }
        }

        public string ExecutablePath
        {
            get => CurrentProfile?.ArgumentSettings?.ExecutablePath;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.ExecutablePath)) return;
                CurrentProfile.ArgumentSettings.ExecutablePath = value;
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
            get => CurrentProfile?.ServerConfig.IntervalBetweenWelcomeMessages;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.IntervalBetweenWelcomeMessages)) return;
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
                var ips = CurrentProfile?.ServerConfig.HeadlessClientIps;
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
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.HeadlessClientIps)) return;
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
                var ips = CurrentProfile?.ServerConfig.LocalClientIps;
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
                    if(value != null)
                    {
                        value = Regex.Replace(value, @"\s+", "");
                    }
                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.LocalClientIps)) return;
                    CurrentProfile.ServerConfig.LocalClientIps = valueAsArray.ToList();
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ??
                       (_browseCommand = new RelayCommand(_ =>
                       {
                           using (var fileDialog = new OpenFileDialog())
                           {
                               if (fileDialog.ShowDialog() != DialogResult.OK)
                               {
                                   return;
                               }

                               ExecutablePath = fileDialog.FileName;
                           }
                       }));
            }
        }
        private ICommand _browseCommand;

        public GeneralViewModel(ServerViewModel viewModel)
        {
            _parentViewModel = viewModel;
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
                    case nameof(Port) when string.IsNullOrWhiteSpace(Port):
                        return "Port can't be null or empty.";
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
                if(string.IsNullOrWhiteSpace(Name))
                {
                    return " Name of the server must be specified.";
                }
                else if(string.IsNullOrWhiteSpace(Port))
                {
                    return "Port must be specified.";
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
