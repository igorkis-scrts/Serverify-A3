using A3ServerTool.Enums;
using A3ServerTool.Helpers;
using GalaSoft.MvvmLight;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;

namespace A3ServerTool.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public List<CultureInfo> Cultures => App.Languages;

        public CultureInfo Culture
        {
            get => _culture;
            set
            {
                if (Equals(value, _culture))
                {
                    return;
                }

                _culture = value;
                RaisePropertyChanged();
            }
        }
        private CultureInfo _culture;

        /// <summary>
        /// Gets or sets the background theme.
        /// </summary>
        public BackgroundThemeType BackgroundTheme
        {
            get => _backgroundTheme;
            set
            {
                if (Equals(value, _backgroundTheme))
                {
                    return;
                }

                _backgroundTheme = value;
                RaisePropertyChanged();
            }
        }
        private BackgroundThemeType _backgroundTheme;

        /// <summary>
        /// Gets or sets the color of the accent.
        /// </summary>
        public AccentColorType AccentColor
        {
            get => _accentColor;
            set
            {
                if (Equals(value, _accentColor))
                {
                    return;
                }

                _accentColor = value;
                RaisePropertyChanged();
            }
        }
        private AccentColorType _accentColor;


        /// <summary>
        /// Gets the window loaded command.
        /// </summary>
        public ICommand WindowLoadedCommand => _windowLoadedCommand ??
                       (_windowLoadedCommand = new RelayCommand(_ =>
                       {
                       }));
        private ICommand _windowLoadedCommand;


        /// <summary>
        /// Gets the save profile command.
        /// </summary>
        public ICommand SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ??
                       (_saveSettingsCommand = new RelayCommand(_ =>
                       {
                           ApplyAndSaveSettings();
                       }));
            }
        }
        private ICommand _saveSettingsCommand;

        public SettingsViewModel()
        {
            Culture = Properties.Settings.Default.Language;
            Enum.TryParse(Properties.Settings.Default.BackgroundTheme, out BackgroundThemeType backgroundTheme);
            BackgroundTheme = backgroundTheme;
            Enum.TryParse(Properties.Settings.Default.AccentColor, out AccentColorType accentColor);
            AccentColor = accentColor;
        }

        /// <summary>
        /// Saves the language settings.
        /// </summary>
        private void ApplyAndSaveSettings()
        {
            App.Language = Culture;
            App.BackgroundTheme = ThemeManager.GetAppTheme(BackgroundTheme.ToString());
            App.AccentColor = ThemeManager.GetAccent(AccentColor.ToString());
        }
    }
}
