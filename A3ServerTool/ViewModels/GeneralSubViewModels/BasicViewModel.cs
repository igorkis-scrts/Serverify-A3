using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


namespace A3ServerTool.ViewModels.GeneralSubViewModels
{
    public class BasicViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly GeneralViewModel _parentViewModel;

        public BasicConfig CurrentBasicConfig => _parentViewModel.CurrentProfile.BasicConfig;

        public int? MaxMessagesSend
        {
            get => CurrentBasicConfig.MaxMessagesSend;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxMessagesSend)) return;
                CurrentBasicConfig.MaxMessagesSend = value;
                RaisePropertyChanged();
            }
        }

        public int? MaxSizeGuaranteed
        {
            get => CurrentBasicConfig.MaxSizeGuaranteed;
            set
            {
                if (Equals(value, CurrentBasicConfig.MaxSizeGuaranteed)) return;
                CurrentBasicConfig.MaxSizeGuaranteed = value;
                RaisePropertyChanged();
            }
        }

        public BasicViewModel(GeneralViewModel viewModel)
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
                    case nameof(MaxMessagesSend) when MaxMessagesSend < 0:
                        return "MaxMessagesSend must be more than zero.";
                    case nameof(MaxSizeGuaranteed) when MaxSizeGuaranteed < 0:
                        return "MaxSizeGuaranteed must be more than zero.";
                    default:
                        return null;
                }
            }
        }

        public string Error
        {
            get
            {
                if(MaxMessagesSend < 0 || MaxMessagesSend == null)
                {
                    return "MaxMessagesSend must be more than zero.";
                }
                if (MaxSizeGuaranteed < 0 || MaxSizeGuaranteed == null)
                {
                    return "MaxSizeGuaranteed must be more than zero.";
                }
                return string.Empty;
            }
        }

        #endregion
    }
}
