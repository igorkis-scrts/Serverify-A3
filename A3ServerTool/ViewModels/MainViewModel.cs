using System;
using System.Windows;
using System.Windows.Input;
using A3ServerTool.Helpers;
using A3ServerTool.Models.Profile;
using A3ServerTool.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Interchangeable.Enums;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace A3ServerTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public const string Token = nameof(MainViewModel);

        private readonly IDialogCoordinator _dialogCoordinator = DialogCoordinator.Instance;
        private readonly CustomDialog _customDialog = new CustomDialog();
        private readonly IProfileDirector _profileDirector;

        /// <summary>
        /// Gets the application title.
        /// </summary>
        public string AppTitle { get; } = Properties.Settings.Default.AppTitle;

        public Profile CurrentProfile
        {
            get => _currentProfile;
            set
            {
                //if (Equals(value, _currentProfile)) return;
                _currentProfile = value;
                RaisePropertyChanged();
            }
        }
        private Profile _currentProfile;

        /// <summary>
        /// Gets the exit application command.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return _exitApplicationCommand ??= new RelayCommand(async _ =>
                {
                    var dialogSettings = new MetroDialogSettings
                    {
                        AffirmativeButtonText = "Quit",
                        NegativeButtonText = "Cancel",
                        AnimateShow = true,
                        AnimateHide = true,
                        ColorScheme = MetroDialogColorScheme.Theme
                    };

                    var dialogResult = await _dialogCoordinator.ShowMessageAsync(this, "Confirm exit",
                        "Are you sure that you want to quit?",
                        MessageDialogStyle.AffirmativeAndNegative, dialogSettings);

                    if (dialogResult != MessageDialogResult.Affirmative) return;
                    
                    Application.Current.Shutdown();
                });
            }
        }
        private ICommand _exitApplicationCommand;

        /// <summary>
        /// Sets the actions that will be executed after form initialization.
        /// </summary>
        public ICommand WindowInitializedCommand
        {
            get
            {
                Messenger.Default.Register<SaveDialogResult<Profile>>(this, Token, ProcessMessageResult);

                var lastProfileId = Properties.Settings.Default.LastUsedProfile;
                if (lastProfileId != Guid.Empty)
                {
                    CurrentProfile = _profileDirector.GetById(lastProfileId);
                    if (CurrentProfile == null)
                    {
                        CurrentProfile = new Profile(Guid.NewGuid());
                        _profileDirector.SetDefaultValues(CurrentProfile);
                    }
                }
                else
                {
                    CurrentProfile = new Profile(Guid.NewGuid());
                }

                //for some reason WPF refuses to execute code wrapped into relay command after initialization
                return new RelayCommand(_ => {});
            }
        }

        /// <summary>
        /// Saves current profile.
        /// </summary>
        public ICommand SaveProfileCommand
        {
            get
            {
                return _saveProfileCommand ??= new RelayCommand(async _ =>
                {
                    if (_profileDirector.ExistOnStorage(CurrentProfile))
                    {
                        _profileDirector.SaveStorage(CurrentProfile);

                        var dialogSettings = new MetroDialogSettings
                        {
                            AffirmativeButtonText = "OK",
                            ColorScheme = MetroDialogColorScheme.Accented
                        };

                        await ((MetroWindow)Application.Current.MainWindow)
                            .ShowMessageAsync(Properties.StaticLang.SuccessTitle, Properties.StaticLang.SuccessfulSavedProfileText, MessageDialogStyle.Affirmative, dialogSettings);
                    }
                    else
                    {
                        _customDialog.Content = new ProfileDialogView();
                        await _dialogCoordinator.ShowMetroDialogAsync(this, _customDialog);
                        Messenger.Default.Send(CurrentProfile);
                        Messenger.Default.Send(ViewMode.Save);
                    }
                });
            }
        }
        private ICommand _saveProfileCommand;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IProfileDirector director)
        {
            _profileDirector = director;
        }

        /// <summary>
        /// Processes the message result (profile creation dialog).
        /// </summary>
        /// <param name="messageContent">Content of the message.</param>
        private async void ProcessMessageResult(SaveDialogResult<Profile> messageContent)
        {
            var dialogResult = messageContent;
            await _dialogCoordinator.HideMetroDialogAsync(this, _customDialog);

            if (dialogResult.Message == MessageDialogResult.Affirmative)
            {
                _profileDirector.SaveStorage(dialogResult.Object);
                CurrentProfile = dialogResult.Object;
                Properties.Settings.Default.LastUsedProfile = CurrentProfile.Id;
                Properties.Settings.Default.Save();
                Messenger.Default.Send("update", Token);
            }
        }
    }
}