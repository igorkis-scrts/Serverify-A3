using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using Interchangeable;

namespace A3ServerTool.ViewModels
{
    public class GeneralViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        public Profile CurrentProfile => _mainViewModel.CurrentProfile;

        public string Port
        {
            get => CurrentProfile?.ServerSettings?.Port;
            set
            {
                if (Equals(value, CurrentProfile.ServerSettings.Port)) return;
                CurrentProfile.ServerSettings.Port = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => CurrentProfile?.ServerSettings?.Name;
            set
            {
                if (Equals(value, CurrentProfile.ServerSettings.Name)) return;
                CurrentProfile.ServerSettings.Name = value;
                RaisePropertyChanged();
            }
        }

        public string ExecutablePath
        {
            get => CurrentProfile?.ServerSettings?.ExecutablePath;
            set
            {
                if (Equals(value, CurrentProfile.ServerSettings.ExecutablePath)) return;
                CurrentProfile.ServerSettings.ExecutablePath = value;
                RaisePropertyChanged();
            }
        }

        public GeneralViewModel(MainViewModel viewModel)
        {
            _mainViewModel = viewModel;
        }

        private ICommand _browseCommand;

        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ??
                       (_browseCommand = new RelayCommand(obj =>
                       {
                           using (var fileDialog = new OpenFileDialog())
                           {
                               if (fileDialog.ShowDialog() != DialogResult.OK)
                               {
                                   return;
                               }

                               ExecutablePath = fileDialog.FileName;
                           }
                       })); //TODO: Validate main fields));
            }
        }

        private ICommand _startServerCommand;

        public ICommand StartServerCommand
        {
            get
            {
                return _startServerCommand ??
                       (_startServerCommand = new RelayCommand( obj =>
                           {
                               //temporary
                               new ServerLauncher().Run(Port, Name, ExecutablePath);
                           }, obj => ExecutablePath != null));
            }
        }
    }
}
