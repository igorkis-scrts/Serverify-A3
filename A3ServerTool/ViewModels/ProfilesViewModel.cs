using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using A3ServerTool.Models;
using A3ServerTool.Views;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.ViewModels
{
    public class ProfilesViewModel : PropertyChangedViewModel
    {
        private readonly MainViewModel _mainViewModel;

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly ProfileDialogView _profileDialogView = new ProfileDialogView();
        private readonly CustomDialog _customDialog = new CustomDialog();

        private bool _isRegistered;

        public ObservableCollection<Profile> Profiles { get; set; }


        public ProfilesViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        //A tricky (and a bit messy) way to beat the 
        //"Context is not registered. Consider using DialogParticipation.Register in XAML to bind in the DataContext" error
        //We can't register methods in constructor, so we should do it when userControl is rendered
        //https://stackoverflow.com/questions/41663538/trouble-with-showing-a-mahapps-metro-dialog-with-a-reactiveui-command
        //TODO: better way to create listener only one time
        public ICommand StartupCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (_isRegistered) return;

                    Messenger.Default.Register<Tuple<MessageDialogResult, Profile>>(this, ProcessMessage);
                    _isRegistered = true;
                });
            }
        }

        private Tuple<MessageDialogResult, Profile> _dialogResult;
        public Tuple<MessageDialogResult, Profile> DialogResult
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
            _customDialog.Content = _profileDialogView;;
            await _dialogCoordinator.ShowMetroDialogAsync(this, _customDialog);
        }

        private async void ProcessMessage(Tuple<MessageDialogResult, Profile> messageContent)
        {
           DialogResult = messageContent;
           await _dialogCoordinator.HideMetroDialogAsync(this, _customDialog);

            if (DialogResult.Item1 == MessageDialogResult.Affirmative)
            {
                //TODO: add object to observable collection, asynchrously save it to local storage
            }
        }
    }
}
