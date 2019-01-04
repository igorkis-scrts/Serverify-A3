using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using A3ServerTool.Models;
using A3ServerTool.Views;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace A3ServerTool.ViewModels
{
    public class ProfilesViewModel : PropertyChangedViewModel
    {
        private readonly MainViewModel _mainViewModel;

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly CreateProfileDialog _createDialog = new CreateProfileDialog();
        private readonly CustomDialog _customDialog = new CustomDialog();

        public ObservableCollection<Profile> Profiles { get; set; }

        public ProfilesViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            //To recieve 
            //Messenger.Default.Register<string>(this, ProcessMessage);
            //_mainViewModel = mainViewModel;
        }

        private string _dialogResult;
        public string DialogResult
        {
            get => _dialogResult;
            set
            {
                if (_dialogResult == value)
                {
                    return;
                }
                _dialogResult = value;
                OnPropertyChanged();
            }
        }


        private ICommand _createProfileCommand;

        public ICommand CreateProfileCommand
        {
            get
            {
                return _createProfileCommand ??
                       (_createProfileCommand = new RelayCommand(obj =>
                       {
                           ShowDialog();
                       }));
            }
        }





        private async void ShowDialog()
        {
            _customDialog.Content = _createDialog;
            await _dialogCoordinator.ShowMetroDialogAsync(this, _customDialog);
        }

        //TODO: Obviously, this object should return profile
        private async void ProcessMessage(string messageContent)
        {
            DialogResult = messageContent;
            await _dialogCoordinator.HideMetroDialogAsync(this, _customDialog);
        }
    }
}
