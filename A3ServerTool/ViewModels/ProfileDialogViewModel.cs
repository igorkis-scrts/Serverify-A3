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

        //public bool HasError { get; set; }

        private string _profileName;

        /// <summary>
        /// Profile name
        /// </summary>
        public string ProfileName
        {
            get => _profileName;
            set
            {
                if (Equals(value, _profileName))
                {
                    return;
                }

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
                if (Equals(value, _profileDescription))
                {
                    return;
                }

                _profileDescription = value;
                OnPropertyChanged();
            }
        }

        private ProfileType _profileType;

        /// <summary>
        /// Profile type (game)
        /// </summary>
        public ProfileType ProfileType
        {
            get => _profileType;
            set
            {
                if (Equals(value, _profileType))
                {
                    return;
                }

                _profileType = value;
                OnPropertyChanged();
            }
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
                           //var qq = this[nameof(ProfileName)];
                           //if (HasErrors) return;

                           //SendMessage(MessageDialogResult.Affirmative, new Profile
                           //{
                           //    Description = ProfileDescription,
                           //    Name = ProfileName,
                           //    Type = ProfileType
                           //});
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

        #endregion

        #region Private methods

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
