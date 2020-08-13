using A3ServerTool.Helpers.ServerLauncher;
using A3ServerTool.Models;
using A3ServerTool.ViewModels.ServerSubViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable.IO;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using A3ServerTool.Models.Profile;

namespace A3ServerTool.ViewModels
{
    public class ServerViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IServerLauncher _launcher;
        private readonly IProfileDirector _profileDirector;

        public Profile CurrentProfile => _mainViewModel.CurrentProfile;

        public ServerViewModel(MainViewModel viewModel, IServerLauncher launcher, IProfileDirector profileDirector)
        {
            _mainViewModel = viewModel;
            _launcher = launcher;
            _profileDirector = profileDirector;
        }

        public ICommand StartServerCommand
        {
            get
            {
                return _startServerCommand ??= new RelayCommand(async _ =>
                {
                    if (!FileHelper.CheckFileExistence(CurrentProfile.ExecutablePath))
                    {
                        var dialogSettings = new MetroDialogSettings
                        {
                            AffirmativeButtonText = "OK",
                            ColorScheme = MetroDialogColorScheme.Accented
                        };

                        await ((MetroWindow)Application.Current.MainWindow)
                            .ShowMessageAsync("Error", "Server executable not exists on specified path.", MessageDialogStyle.Affirmative, dialogSettings).ConfigureAwait(false);
                    }
                    else
                    {
                        await SaveProfile();
                        _launcher.Run(CurrentProfile);
                        Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
                    }
                }, _ => CheckValidation());
            }
        }

        private ICommand _startServerCommand;

        /// <summary>
        /// Saves the profile.
        /// </summary>
        private Task SaveProfile()
        {
            return Task.Run(() =>
            {
                if(!_profileDirector.ExistOnStorage(CurrentProfile))
                {
                    CurrentProfile.Name = "Default Profile";
                }
                _profileDirector.SaveStorage(CurrentProfile);
                Properties.Settings.Default.LastUsedProfile = CurrentProfile.Id;
                Properties.Settings.Default.Save();
                Messenger.Default.Send("update", MainViewModel.Token);
            });
        }

        /// <summary>
        /// Checks the validation.
        /// </summary>
        private bool CheckValidation()
        {
            var generalViewModel = SimpleIoc.Default.GetInstance<GeneralViewModel>();
            var networkViewModel = SimpleIoc.Default.GetInstance<NetworkViewModel>();
            return string.IsNullOrWhiteSpace(generalViewModel.Error) && string.IsNullOrWhiteSpace(networkViewModel.Error);
        }
    }
}
