using A3ServerTool.Helpers;
using GalaSoft.MvvmLight;
using Interchangeable;
using Interchangeable.Enums;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Windows.Input;

namespace A3ServerTool.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets application language.
        /// </summary>
        public Language Language
        {
            get => _language;
            set
            {
                if (Equals(value, _language))
                {
                    return;
                }

                _language = value;
                RaisePropertyChanged();
            }
        }
        private Language _language;

        /// <summary>
        /// Gets the window loaded command.
        /// </summary>
        public ICommand WindowLoadedCommand => _windowLoadedCommand ??
                       (_windowLoadedCommand = new RelayCommand(_ =>
                       {
                           var language = (CultureInfo) SettingsCoordinator.Retrieve(ApplicationSettingType.DefaultLanguage);

                           //TODO: temporary solution, should get rid of Language enum entirely
                           if (language.ToString() == "ru-RU")
                           {
                               Language = Language.Russian;
                           }
                           else
                           {
                               Language = Language.English;
                           }
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
                       (_saveSettingsCommand = new RelayCommand(async _ =>
                       {
                           var tiedCulture = Language.GetType()
                                .GetField(Language.ToString())
                                .GetCustomAttributes(typeof(RepresentedCultureInfo), false)
                                .Cast<RepresentedCultureInfo>()
                                .FirstOrDefault();

                           App.Language = new CultureInfo(tiedCulture.Culture);
                           SettingsCoordinator.Save(ApplicationSettingType.DefaultLanguage, App.Language);
                       }));
            }
        }
        private ICommand _saveSettingsCommand;
    }
}
