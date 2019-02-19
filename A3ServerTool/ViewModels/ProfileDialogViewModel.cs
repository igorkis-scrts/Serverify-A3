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
using Interchangeable;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// Profile creation/editing dialog
    /// </summary>
    public class ProfileDialogViewModel : ViewModelBase, IDataErrorInfo
    {

        #region Properties and fields

        private Profile _profile = new Profile();

        /// <summary>
        /// Flag to unregister view model when we don't need it
        /// </summary>
        public bool IsExpired { get; set; }

        private bool _isEditMode;
        /// <summary>
        /// Is dialog in edit form
        /// </summary>
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (Equals(value, _isEditMode))
                {
                    return;
                }

                _isEditMode = value;
                RaisePropertyChanged();
            }
        }

        private bool _hasToSave;
        /// <summary>
        /// Flag to check if current profile is needed to be saved
        /// </summary>
        public bool HasToSaveCurrentProfile
        {
            get => _hasToSave;
            set
            {
                if (Equals(value, _hasToSave))
                {
                    return;
                }

                _hasToSave = value;
                RaisePropertyChanged();
            }
        }

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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public ProfileDialogViewModel()
        {
            Messenger.Default.Register<ViewMode>(this, HandleViewState);
            Messenger.Default.Register<Profile>(this, RecieveProfile);
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
                           if (!HasToSaveCurrentProfile)
                           {
                               switch(ProfileType)
                               {
                                   case (ProfileType.Arma3):
                                   _profile.ServerSettings = new A3ServerSettings();
                                   break;

                                   case ProfileType.Dayz:
                                   default:
                                   throw new NotImplementedException();
                               }
                           }

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
            if (!HasToSaveCurrentProfile)
            {
                //switch (ProfileType)
                //{
                //    case ProfileType.Arma3:
                //        profile.ServerSettings = new A3ServerSettings();;
                //        break;
                //}
            }

            var result = new DialogResult<Profile>(dialogResult, profile);
            Messenger.Default.Send(result);
        }

        private void RecieveProfile(Profile profile)
        {
            _profile = profile;
        }


        private void HandleViewState(ViewMode mode)
        {
            switch (mode)
            {
                case ViewMode.Edit:
                    HeaderText = "Edit profile";
                    ButtonText = "Edit";
                    IsEditMode = true;
                    break;
                case ViewMode.New:
                    HeaderText = "Create profile";
                    ButtonText = "Create";
                    IsEditMode = false;
                    break;
                case ViewMode.None:
                default:
                    break;
            }
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
