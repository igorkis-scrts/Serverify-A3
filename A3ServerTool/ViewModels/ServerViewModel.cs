using System;
using System.Windows.Input;
using A3ServerTool.Models;
using A3ServerTool.ViewModels.ServerSubViewModels;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IServerLauncher _launcher;

        public Profile CurrentProfile => _mainViewModel.CurrentProfile;

        public ServerViewModel(MainViewModel viewModel, IServerLauncher launcher)
        {
            _mainViewModel = viewModel;
            _launcher = launcher;
        }

        public ICommand StartServerCommand
        {
            get
            {
                return _startServerCommand ??
                       (_startServerCommand = new RelayCommand(_ =>
                          {
                              _launcher.Run(CurrentProfile);
                          }, _ => CheckValidation()));
            }
        }
        private ICommand _startServerCommand;

        private bool CheckValidation()
        {
            var detailsViewModel = ServiceLocator.Current.GetInstance<GeneralViewModel>();
            var basicViewModel = ServiceLocator.Current.GetInstance<NetworkViewModel>();
            return string.IsNullOrEmpty(detailsViewModel.Error) && string.IsNullOrEmpty(basicViewModel.Error);
        }
    }
}
