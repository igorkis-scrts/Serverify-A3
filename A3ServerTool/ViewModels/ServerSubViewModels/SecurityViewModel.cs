using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Provides VM for handling server security properties.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class SecurityViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets password required to get admin rights on server. 
        /// </summary>
        public string AdminPassword
        {
            get => CurrentProfile?.ServerConfig.AdminPassword;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.AdminPassword)) return;
                CurrentProfile.ServerConfig.AdminPassword = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the admin IDs.
        /// </summary>
        public string AdminIds
        {
            get
            {
                var ids = CurrentProfile?.ServerConfig.AdminUids;
                if (ids != null)
                {
                    return string.Join(",", ids);
                }
                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.AdminUids = null;
                }
                else
                {
                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.AdminUids)) return;
                    CurrentProfile.ServerConfig.AdminUids = valueAsArray.ToList();
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the kick duplicate uids.
        /// </summary>
        public int? KickDuplicateUids
        {
            get => CurrentProfile?.ServerConfig.KickDuplicateIds;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.KickDuplicateIds)) return;
                CurrentProfile.ServerConfig.KickDuplicateIds = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required by alternate syntax of serverCommand server-side scripting. . 
        /// </summary>
        public string ServerCommandPassword
        {
            get => CurrentProfile?.ServerConfig.ServerCommandPassword;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.ServerCommandPassword)) return;
                CurrentProfile.ServerConfig.ServerCommandPassword = value;
                RaisePropertyChanged();
            }
        }

        public SecurityViewModel(ServerViewModel viewModel)
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
                    case nameof(AdminIds) when !string.IsNullOrWhiteSpace(AdminIds) && Regex.Matches(AdminIds, @"[a-zA-Z]").Count > 0:
                        return "Admin identifiers can be only numbers.";
                    default:
                        return null;
                }
            }
        }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
