using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public LoggingViewModel(ServerViewModel viewModel)
        {
            _parentViewModel = viewModel;
        }

        public string this[string columnName] => string.Empty;

        public string Error => string.Empty;
    }
}
