using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace A3ServerTool.ViewModels
{
    public class GeneralViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        public GeneralViewModel(MainViewModel viewModel)
        {
            _mainViewModel = viewModel;
        }
    }
}
