using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
