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


namespace A3ServerTool.ViewModels.GeneralSubViewModels
{
    public class BasicViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly GeneralViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public BasicViewModel(GeneralViewModel viewModel)
        {
            _parentViewModel = viewModel;
        }

        #region IDataErrorInfo

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        #endregion
    }
}
