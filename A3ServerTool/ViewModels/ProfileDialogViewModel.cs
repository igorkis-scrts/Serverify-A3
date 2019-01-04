using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// Profile create/edit dialog
    /// </summary>
    public class ProfileDialogViewModel : PropertyChangedViewModel
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

        private RelayCommand _okCommand;
        
        /// <summary>
        /// Confirm user actions
        /// </summary>
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand
                       ?? (_okCommand = new RelayCommand(obj =>
                       {
                           SendMessage(MessageDialogResult.Affirmative, new Profile
                           {
                                   Description = ProfileDescription,
                                   Name = ProfileName,
                                   Type = ProfileType
                           });
                       }));
            }
        }

        private RelayCommand _cancelCommand;

        /// <summary>
        /// Cancel any user input
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand
                       ?? (_cancelCommand = new RelayCommand(obj =>
                       {    
                           SendMessage(MessageDialogResult.Canceled);
                       }));
            }
        }

        /// <summary>
        /// Send result to parent view model (ProfileViewModel in this case)
        /// </summary>
        /// <param name="dialogResult"></param>
        /// <param name="profile"></param>
        private void SendMessage(MessageDialogResult dialogResult, Profile profile = null)
        {
            var result = new Tuple<MessageDialogResult, Profile>(dialogResult, profile);
            Messenger.Default.Send(result);
        }
    }
}
