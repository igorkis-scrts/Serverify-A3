using A3ServerTool.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;

namespace A3ServerTool.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public List<CultureInfo> Cultures
        {
            get
            {
                return App.Languages;
            }
        }

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
            Culture = (CultureInfo)SettingsCoordinator.Retrieve(ApplicationSettingType.DefaultLanguage);
        }

        /// <summary>
        /// Saves the language settings.
        /// </summary>
        private void ApplyAndSaveSettings()
        {
            App.Language = Culture;
            SettingsCoordinator.Save(ApplicationSettingType.DefaultLanguage, Culture);
        }
    }
}
