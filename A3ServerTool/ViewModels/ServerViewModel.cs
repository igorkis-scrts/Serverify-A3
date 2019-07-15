using A3ServerTool.Helpers.ServerLauncher;
using A3ServerTool.Models;
using A3ServerTool.ViewModels.ServerSubViewModels;
using GalaSoft.MvvmLight;
using Interchangeable.IO;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Input;

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
                              if (!FileHelper.CheckFileExistence(CurrentProfile.ExecutablePath))
                              {
                                  var dialogSettings = new MetroDialogSettings
                                  {
                                      AffirmativeButtonText = "OK",
                                      ColorScheme = MetroDialogColorScheme.Accented
                                  };

                                  ((MetroWindow)Application.Current.MainWindow)
                                     .ShowMessageAsync("Error", "Server executable not exists on specified path.", MessageDialogStyle.Affirmative, dialogSettings);
                              }
                              else
                              {
                                  _launcher.Run(CurrentProfile);
                              }
                          }, _ => CheckValidation()));
            }
        }
        private ICommand _startServerCommand;

        private bool CheckValidation()
        {
            var detailsViewModel = ServiceLocator.Current.GetInstance<GeneralViewModel>();
            var basicViewModel = ServiceLocator.Current.GetInstance<NetworkViewModel>();
            return string.IsNullOrWhiteSpace(detailsViewModel.Error) && string.IsNullOrWhiteSpace(basicViewModel.Error);
        }
    }
}
