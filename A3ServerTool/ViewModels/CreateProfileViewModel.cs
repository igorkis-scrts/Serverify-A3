using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// Profile creation dialog
    /// </summary>
    public class CreateProfileViewModel : PropertyChangedViewModel
    {

        private string _profileName;

        /// <summary>
        /// Profile name
        /// </summary>
        public string ProfileName
        {
            get => _profileName;
            set
            {
                _profileName = value;
                OnPropertyChanged();
            }
        }

        private string _profileDescription;

        /// <summary>
        /// Profile description
        /// </summary>
        public string ProfileDescription
        {
            get => _profileDescription;
            set
            {
                _profileDescription = value;
                OnPropertyChanged();
            }
        }

        private ProfileType _gameType;

        /// <summary>
        /// Profile type (game)
        /// </summary>
        public ProfileType ProfileType
        {
            get => _gameType;
            set
            {
                _gameType = value;
                OnPropertyChanged();
            }
        }

        private string _dialogMessage;
        public string DialogMessage
        {
            get => _dialogMessage;
            set
            {
                if (_dialogMessage == value)
                {
                    return;
                }
                _dialogMessage = value;
                OnPropertyChanged();
            }
        }

        //TODO: Create profile and send it to main view
        private RelayCommand _sendMessageCommand;
        public RelayCommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand
                       ?? (_sendMessageCommand = new RelayCommand(obj =>
                       {
                           SendMessage();
                       }));
            }
        }

        ////TODO: Make close button work
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand
                       ?? (_closeCommand = new RelayCommand(obj =>
                       {
                           //this.
                       }));
            }
        }


        private void SendMessage()
        {
            var profile = new Profile
            {
                Description = ProfileDescription,
                Name = ProfileName,
                Type = ProfileType
            };
            //Messenger.Default.Send(DialogMessage);
            Messenger.Default.Send(profile);
        }
    }
}
