using System;
using System.Windows.Input;
using A3ServerTool.Models;
using A3ServerTool.ViewModels.GeneralSubViewModels;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class GeneralViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        public Profile CurrentProfile => _mainViewModel.CurrentProfile;

        public GeneralViewModel(MainViewModel viewModel)
        {
            _mainViewModel = viewModel;
        }

        public ICommand StartServerCommand
        {
            get
            {
                return _startServerCommand ??
                       (_startServerCommand = new RelayCommand(obj =>
                          {
                          }, obj => CheckValidation()));
            }
        }
        private ICommand _startServerCommand;


        private bool CheckValidation()
        {
            var detailsViewModel = ServiceLocator.Current.GetInstance<DetailsViewModel>();
            return string.IsNullOrEmpty(detailsViewModel.Error);
        }
    }
}
