using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using A3ServerTool.Enums;
using A3ServerTool.Models.Profile;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable;

namespace A3ServerTool.ViewModels.ServerSubViewModels
{
    /// <summary>
    /// Provides VM for handling server security properties.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class SecurityViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ServerViewModel _parentViewModel;

        private Profile CurrentProfile => _parentViewModel.CurrentProfile;

        /// <summary>
        /// Gets or sets password required to get admin rights on server. 
        /// </summary>
        public string AdminPassword
        {
            get => CurrentProfile?.ServerConfig?.AdminPassword;
            set
            {
                if (Equals(value, CurrentProfile?.ServerConfig?.AdminPassword)) return;
                if (CurrentProfile?.ServerConfig != null) CurrentProfile.ServerConfig.AdminPassword = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the admin IDs.
        /// </summary>
        public string AdminIds
        {
            get
            {
                var ids = CurrentProfile?.ServerConfig?.AdminUids;
                if (ids != null)
                {
                    return string.Join(",", ids);
                }

                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.AdminUids = null;
                }
                else
                {
                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.AdminUids)) return;
                    if (CurrentProfile != null) CurrentProfile.ServerConfig.AdminUids = valueAsArray.ToList();
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the kick duplicate uids.
        /// </summary>
        public int? KickDuplicateUids
        {
            get => CurrentProfile?.ServerConfig.KickDuplicateIds;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.KickDuplicateIds)) return;
                CurrentProfile.ServerConfig.KickDuplicateIds = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required by alternate syntax of serverCommand server-side scripting. . 
        /// </summary>
        public string ServerCommandPassword
        {
            get => CurrentProfile?.ServerConfig.ServerCommandPassword;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.ServerCommandPassword)) return;
                CurrentProfile.ServerConfig.ServerCommandPassword = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required by alternate syntax of serverCommand server-side scripting. 
        /// </summary>
        public FilePatchingType FilePatchingType
        {
            get =>
                CurrentProfile?.ServerConfig?.FilePatchingMode != null
                    ? (FilePatchingType) CurrentProfile.ServerConfig.FilePatchingMode
                    : FilePatchingType.NoClients;
            set
            {
                if (Equals((int) value, CurrentProfile?.ServerConfig?.FilePatchingMode)) return;
                if (CurrentProfile?.ServerConfig != null) CurrentProfile.ServerConfig.FilePatchingMode = (int) value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the welcome messages.
        /// </summary>
        public string FilePatchingExceptions
        {
            get
            {
                var messages = CurrentProfile?.ServerConfig.FilePatchingExceptions;
                if (messages != null)
                {
                    return string.Join(",", messages);
                }

                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.FilePatchingExceptions = null;
                }
                else
                {
                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.FilePatchingExceptions)) return;
                    if (CurrentProfile != null)
                    {
                        CurrentProfile.ServerConfig.FilePatchingExceptions = valueAsArray.ToList();
                    }

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets action when second user with same ID is detected.
        /// </summary>
        public string DoubleIdScript
        {
            get => CurrentProfile?.ServerConfig.OnDoubleIdCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnDoubleIdCommand)) return;
                CurrentProfile.ServerConfig.OnDoubleIdCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets action when user connected to the server.
        /// </summary>
        public string UserConnectedScript
        {
            get => CurrentProfile?.ServerConfig.OnUserConnectedCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnUserConnectedCommand)) return;
                CurrentProfile.ServerConfig.OnUserConnectedCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the user disconnected script.
        /// </summary>
        public string UserDisconnectedScript
        {
            get => CurrentProfile?.ServerConfig.OnUserDisconnectedCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnUserDisconnectedCommand)) return;
                CurrentProfile.ServerConfig.OnUserDisconnectedCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the hacked data script.
        /// </summary>
        public string HackedDataScript
        {
            get => CurrentProfile?.ServerConfig.OnHackedDataCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnHackedDataCommand)) return;
                CurrentProfile.ServerConfig.OnHackedDataCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the different data script.
        /// </summary>
        public string DifferentDataScript
        {
            get => CurrentProfile?.ServerConfig.OnDifferentDataCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnDifferentDataCommand)) return;
                CurrentProfile.ServerConfig.OnDifferentDataCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the unsigned data script.
        /// </summary>
        public string UnsignedDataScript
        {
            get => CurrentProfile?.ServerConfig.OnUnsignedDataCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnUnsignedDataCommand)) return;
                CurrentProfile.ServerConfig.OnUnsignedDataCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the user kicked script.
        /// </summary>
        public string UserKickedScript
        {
            get => CurrentProfile?.ServerConfig.OnUserKickedCommand;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.OnUserKickedCommand)) return;
                CurrentProfile.ServerConfig.OnUserKickedCommand = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets password required by alternate syntax of serverCommand server-side scripting.
        /// </summary>
        public SignatureVerificationType SignatureVerificationType
        {
            get => CurrentProfile?.ServerConfig?.SignatureVerificationMode != null
                ? (SignatureVerificationType) CurrentProfile.ServerConfig.SignatureVerificationMode
                : SignatureVerificationType.SecondVersion;
            set
            {
                if (Equals((int) value, CurrentProfile.ServerConfig.SignatureVerificationMode)) return;
                CurrentProfile.ServerConfig.SignatureVerificationMode = (int) value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has Battle Eye anticheat engine.
        /// </summary>
        public bool HasBattleEye
        {
            get => CurrentProfile == null || CurrentProfile.ServerConfig == null
                ? false
                : CurrentProfile.ServerConfig.HasBattleEye;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.HasBattleEye)) return;
                CurrentProfile.ServerConfig.HasBattleEye = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has Battle Eye anticheat engine.
        /// </summary>
        public bool IsFilePatchingAllowed
        {
            get => CurrentProfile?.ArgumentSettings?.IsFilePatchingEnabled ?? false;
            set
            {
                if (Equals(value, CurrentProfile.ArgumentSettings.IsFilePatchingEnabled)) return;
                CurrentProfile.ArgumentSettings.IsFilePatchingEnabled = value;
                Messenger.Default.Send("UpdateFinalString", GeneralViewModel.Token);
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets required build of the game to connect to the server.
        /// </summary>
        public int? RequiredBuild
        {
            get => CurrentProfile?.ServerConfig.RequiredBuild;
            set
            {
                if (Equals(value, CurrentProfile.ServerConfig.RequiredBuild)) return;
                CurrentProfile.ServerConfig.RequiredBuild = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the load file extensions.
        /// </summary>
        public string LoadFileExtensions
        {
            get
            {
                var extensions = CurrentProfile?.ServerConfig.LoadFileExtensionsWhitelist;
                if (extensions != null)
                {
                    return string.Join(",", extensions);
                }

                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.LoadFileExtensionsWhitelist = null;
                }
                else
                {
                    value = Regex.Replace(value, @"\s+", string.Empty);

                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.LoadFileExtensionsWhitelist)) return;
                    if (CurrentProfile != null)
                    {
                        CurrentProfile.ServerConfig.LoadFileExtensionsWhitelist = valueAsArray.ToList();
                    }
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the HTML file extensions.
        /// </summary>
        public string HtmlFileExtensions
        {
            get
            {
                var extensions = CurrentProfile?.ServerConfig.HtmlFileExtensionsWhitelist;
                if (extensions != null)
                {
                    return string.Join(",", extensions);
                }

                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.HtmlFileExtensionsWhitelist = null;
                }
                else
                {
                    value = Regex.Replace(value, @"\s+", string.Empty);

                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.HtmlFileExtensionsWhitelist)) return;
                    if (CurrentProfile != null)
                    {
                        CurrentProfile.ServerConfig.HtmlFileExtensionsWhitelist = valueAsArray.ToList();
                    }
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the preprocess file extensions.
        /// </summary>
        public string PreprocessFileExtensions
        {
            get
            {
                var extensions = CurrentProfile?.ServerConfig.PreprocessFileExtensionsWhitelist;
                if (extensions != null)
                {
                    return string.Join(",", extensions);
                }

                return null;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CurrentProfile.ServerConfig.PreprocessFileExtensionsWhitelist = null;
                }
                else
                {
                    value = Regex.Replace(value, @"\s+", string.Empty);

                    var valueAsArray = value?.Split(',');
                    if (Equals(valueAsArray, CurrentProfile?.ServerConfig.PreprocessFileExtensionsWhitelist)) return;
                    if (CurrentProfile != null)
                    {
                        CurrentProfile.ServerConfig.PreprocessFileExtensionsWhitelist = valueAsArray.ToList();
                    }
                    RaisePropertyChanged();
                }
            }
        }

        public SecurityViewModel(ServerViewModel viewModel)
        {
            _parentViewModel = viewModel;
        }

        public void IsNumericInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = RegexPresetValues.OnlyNumeric.IsMatch(e.Text);
        }

        public void IsNumericWithCommasInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !RegexPresetValues.OnlyNumericWithCommas.IsMatch(e.Text);
        }

        public void IsLettersWithCommasInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !RegexPresetValues.OnlyLettersWithCommas.IsMatch(e.Text);
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(AdminIds) when !string.IsNullOrWhiteSpace(AdminIds) &&
                                               Regex.Matches(AdminIds, @"[a-zA-Z]").Count > 0:
                        return "Admin identifiers can be only numbers.";
                    default:
                        return null;
                }
            }
        }

        public string Error => string.Empty;

        #endregion
    }
}