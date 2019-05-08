using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Represents view model with logging server properties.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class LoggingViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets the call extension report limit.
        /// </summary>
        public float? CallExtensionReportLimit
        {
            get => CurrentProfile?.ServerConfig.CallExtensionReportLimit;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig.CallExtensionReportLimit)) return;
                CurrentProfile.ServerConfig.CallExtensionReportLimit = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath
        {
            get => CurrentProfile?.ServerConfig?.LogFileName;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig?.LogFileName)) return;
                CurrentProfile.ServerConfig.LogFileName = !value.Contains(".log")
                    ? value + ".log"
                    : value;
                RaisePropertyChanged();
            }
        }

        public LoggingViewModel(ServerViewModel viewModel)
        {
            _parentViewModel = viewModel;
        }

        public string this[string columnName] => string.Empty;

        public string Error => string.Empty;
    }
}
