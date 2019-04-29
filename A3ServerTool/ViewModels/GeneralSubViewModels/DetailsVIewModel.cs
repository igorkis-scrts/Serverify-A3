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
    public class DetailsViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly GeneralViewModel _parentViewModel;

        public Profile CurrentProfile => _parentViewModel.CurrentProfile;

        public string Port
        {
            get => CurrentProfile?.ArgumentSettings?.Port;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.Port)) return;
                CurrentProfile.ArgumentSettings.Port = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => CurrentProfile?.ArgumentSettings?.Name;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.Name)) return;
                CurrentProfile.ArgumentSettings.Name = value;
                RaisePropertyChanged();
            }
        }

        public string ExecutablePath
        {
            get => CurrentProfile?.ArgumentSettings?.ExecutablePath;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.ExecutablePath)) return;
                CurrentProfile.ArgumentSettings.ExecutablePath = value;
                RaisePropertyChanged();
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return _browseCommand ??
                       (_browseCommand = new RelayCommand(_ =>
                       {
                           using (var fileDialog = new OpenFileDialog())
                           {
                               if (fileDialog.ShowDialog() != DialogResult.OK)
                               {
                                   return;
                               }

                               ExecutablePath = fileDialog.FileName;
                           }
                       })); //TODO: Validate main fields
            }
        }
        private ICommand _browseCommand;

        public DetailsViewModel(GeneralViewModel viewModel)
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
                    case nameof(Name) when string.IsNullOrWhiteSpace(Name):
                        return "Name can't be null or empty.";
                    case nameof(Port) when string.IsNullOrWhiteSpace(Port):
                        return "Port can't be null or empty.";
                    case nameof(ExecutablePath) when string.IsNullOrWhiteSpace(ExecutablePath):
                        return "Server path can't be null or empty.";
                    default:
                        return null;
                }
            }
        }

        public string Error
        {
            get
            {
                if(string.IsNullOrWhiteSpace(Name))
                {
                    return " Name of the server must be specified.";
                }
                else if(string.IsNullOrWhiteSpace(Port))
                {
                    return "Port must be specified.";
                }
                else if (string.IsNullOrWhiteSpace(ExecutablePath))
                {
                    return "Server path must be specified.";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}
