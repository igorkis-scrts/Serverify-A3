using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using A3ServerTool.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;
using Interchangeable.Enums;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// Profile creation/editing dialog
    /// </summary>
    public class ProfileDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        private Profile _profile = new Profile(Guid.NewGuid());
        private ViewMode _viewMode;

        /// <summary>
        /// Flag to unregister view model when we don't need it
        /// </summary>
        public bool IsExpired { get; set; }

        private Visibility _visibility;

        /// <summary>
        /// Hide certain controls depending on caller
        /// </summary>
        public Visibility VisibilityState
        {
            get => _visibility;
            set
            {
                if (Equals(value, _visibility))
                {
                    return;
                }

                _visibility = value;
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
        private string _headerText = "Create profile";

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
        private string _buttonText = "Create";

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

        public ProfileDialogViewModel()
        {
            Messenger.Default.Register<ViewMode>(this, HandleViewState);
            Messenger.Default.Register<Profile>(this, RecieveProfile);
        }

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
                               _profile = new Profile(Guid.NewGuid());
                           }

                           SendMessage(MessageDialogResult.Affirmative, _profile);
                           IsExpired = true;
                       }));
            }
        }
        private RelayCommand _okCommand;

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
        private RelayCommand _cancelCommand;

        /// <summary>
        /// Send result to parent view model (ProfileViewModel in this case)
        /// </summary>
        private void SendMessage(MessageDialogResult dialogResult, Profile profile = null)
        {
            var result = new DialogResult<Profile>(dialogResult, profile);

            switch(_viewMode)
            {
                case ViewMode.New:
                case ViewMode.Edit:
                    Messenger.Default.Send(result, "ProfilesViewModel");
                    break;
                case ViewMode.Save:
                    Messenger.Default.Send(result, "MainViewModel");
                    break;
            }
        }

        private void RecieveProfile(Profile profile)
        {
            if(profile != null)
            {
                _profile = profile.Clone() as Profile;
            }

            //TODO: Maybe another, nicer way exists? So many questions, so little anwsers...
            RaisePropertyChanged(nameof(ProfileName));
            RaisePropertyChanged(nameof(ProfileDescription));
        }

        private void HandleViewState(ViewMode mode)
        {
            _viewMode = mode;
            switch (mode)
            {
                case ViewMode.Edit:
                    HeaderText = "Edit profile";
                    ButtonText = "Edit";
                    VisibilityState = Visibility.Collapsed;
                    break;
                case ViewMode.New:
                    HeaderText = "Create profile";
                    ButtonText = "Create";
                    HasToSaveCurrentProfile = true;
                    VisibilityState = Visibility.Visible;
                    break;
                case ViewMode.Save:
                    HeaderText = "Save profile";
                    ButtonText = "Save";
                    HasToSaveCurrentProfile = true;
                    VisibilityState = Visibility.Collapsed;
                    break;
                case ViewMode.None:
                default:
                    break;
            }
        }

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
