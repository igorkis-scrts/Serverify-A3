using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using ControlzEx.Theming;

namespace A3ServerTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public App()
        {
            InitializeComponent();
        }

        public static event EventHandler LanguageChanged;
        public static event EventHandler ThemeChanged;

        private void App_LanguageChanged(object sender, EventArgs e)
        {
            A3ServerTool.Properties.Settings.Default.Language = Language;
            A3ServerTool.Properties.Settings.Default.Save();
        }

        private void App_ThemeChanged(object sender, EventArgs e)
        {
            A3ServerTool.Properties.Settings.Default.Theme = _theme;
            A3ServerTool.Properties.Settings.Default.Save();
            ApplyControlBackground();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptionHandler.Instance.ShowMessage(e.Exception);
            e.Handled = true;
        }

        public static CultureInfo Language
        {
            get => System.Threading.Thread.CurrentThread.CurrentUICulture;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (Equals(value, System.Threading.Thread.CurrentThread.CurrentUICulture)) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                var languageDictionary = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                    case "de-DE":
                        languageDictionary.Source = new Uri($"Resources/Lang.{value.Name}.xaml", UriKind.Relative);
                        break;
                    default:
                        languageDictionary.Source = new Uri("Resources/Lang.xaml", UriKind.Relative);
                        break;
                }

                var oldDictionary = (Application.Current.Resources.MergedDictionaries
                    .Where(d => d.Source != null && d.Source.OriginalString.StartsWith("Resources/Lang.")))
                    .First();
                if (oldDictionary != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
                    Application.Current.Resources.MergedDictionaries.Insert(index, languageDictionary);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(languageDictionary);
                }

                ApplyDot();
                LanguageChanged(Application.Current, EventArgs.Empty);
            }
        }

        public static string Theme
        {
            get => _theme;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                if (Equals(_theme, value))
                {
                    return;
                }

                _theme = value;
                
                ThemeManager.Current.ChangeTheme(Application.Current,  value);
                ThemeChanged(Application.Current, EventArgs.Empty);
            }
        }
        private static string _theme;

        protected override void OnStartup(StartupEventArgs e)
        {
            _theme = A3ServerTool.Properties.Settings.Default.Theme;
            ThemeManager.Current.ChangeTheme(Application.Current,  _theme);
            ApplyControlBackground();

            base.OnStartup(e);

            Languages.Clear();
            Languages.Add(new CultureInfo("en-US"));
            Languages.Add(new CultureInfo("de-DE"));
            Languages.Add(new CultureInfo("ru-RU"));

            LanguageChanged += App_LanguageChanged;
            ThemeChanged += App_ThemeChanged;
            Language = A3ServerTool.Properties.Settings.Default.Language;
            Bindings.Register();
        }

        private void ApplyControlBackground()
        {
            var dictionary = new ResourceDictionary();

            if (A3ServerTool.Properties.Settings.Default.Theme.Contains("Light"))
            {
                dictionary.Source = new Uri("/CustomControls;component/LightThemeColors.xaml", UriKind.Relative);
            }
            else
            {
                dictionary.Source = new Uri("/CustomControls;component/DarkThemeColors.xaml", UriKind.Relative);
            }

            var oldDictionary = Current.Resources.MergedDictionaries.FirstOrDefault(d => 
                d.Source != null && d.Source.OriginalString.Contains("ThemeColors"));

            if(oldDictionary != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
                Application.Current.Resources.MergedDictionaries.Insert(index, dictionary);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        /// <summary>
        /// Applies the dot instead of comma for current culture.
        /// </summary>
        private static void ApplyDot()
        {
            var customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.CurrentCulture = customCulture;
        }
    }
}
