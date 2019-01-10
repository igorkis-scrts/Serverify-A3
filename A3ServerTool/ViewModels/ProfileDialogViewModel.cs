using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class ProfileDialogViewModel : PropertyChangedViewModel, IDataErrorInfo
    {

        #region Properties and fields

        private Profile _profile = new Profile();

        /// <summary>
        /// Flag to unregister view model when we don't need it
        /// </summary>
        public bool IsExpired { get; set; }

        private string _headerText = "Create profile";
        public string HeaderText
        {
            get => _headerText;
            set
            {
                if (Equals(value, _headerText))
                {
                    return;
                }

                _headerText = value;
                OnPropertyChanged();
            }
        }

        private string _buttonText = "Create";
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (Equals(value, _buttonText))
                {
                    return;
                }

                _buttonText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Profile name
        /// </summary>
        public string ProfileName
        {
            get => _profile.Name;
            set
            {
                if (Equals(value, _profile.Name))
                {
                    return;
                }

                _profile.Name = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Profile description
        /// </summary>
        public string ProfileDescription
        {
            get => _profile.Description;
            set
            {
                if (Equals(value, _profile.Description))
                {
                    return;
                }

                _profile.Description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Profile type
        /// </summary>
        public ProfileType ProfileType
        {
            get => _profile.Type;
            set
            {
                if (Equals(value, _profile.Type))
                {
                    return;
                }

                _profile.Type = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public ProfileDialogViewModel()
        {
            Messenger.Default.Register<Profile>
            (
                this,
                RecieveProfile
            );
        }

        #endregion

        #region Commands

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
                           SendMessage(MessageDialogResult.Affirmative, _profile);
                           IsExpired = true;
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
                           IsExpired = true;
                       }));
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Send result to parent view model (ProfileViewModel in this case)
        /// </summary>
        private void SendMessage(MessageDialogResult dialogResult, Profile profile = null)
        {
            var result = new Tuple<MessageDialogResult, Profile>(dialogResult, profile);
            Messenger.Default.Send(result);
        }

        private void RecieveProfile(Profile profile)
        {
            _profile = profile;

            HeaderText = "Edit profile";
            ButtonText = "Edit";
            UpdateView();
        }

        /// <summary>
        /// When profile for edit is recieved 
        /// we need to update the view to populate controls with actual data
        /// </summary>
        /// <remarks>TODO: find better way</remarks>
        private void UpdateView()
        {
            OnPropertyChanged(nameof(ProfileName));
            OnPropertyChanged(nameof(ProfileType));
            OnPropertyChanged(nameof(ProfileDescription));
        }

        #endregion

        #region IDataErrorInfo members

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ProfileName) when string.IsNullOrWhiteSpace(ProfileName):
                        return "Profile name should be not empty";
                    case nameof(ProfileDescription) when string.IsNullOrWhiteSpace(ProfileName):
                    default:
                        return null;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}
