using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
                var valueAsArray = value.Split('\n');
                if (Equals(valueAsArray, CurrentProfile?.ServerConfig.WelcomeMessages)) return;
                CurrentProfile.ServerConfig.WelcomeMessages = valueAsArray;
                RaisePropertyChanged();
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
                       })); //TODO: Validate main fields
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
