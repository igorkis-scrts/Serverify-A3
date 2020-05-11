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
        public static event EventHandler BackgroundThemeChanged;
        public static event EventHandler AccentColorChanged;

        private void App_LanguageChanged(object sender, EventArgs e)
        {
            A3ServerTool.Properties.Settings.Default.Language = Language;
            A3ServerTool.Properties.Settings.Default.Save();
        }

        private void App_BackgroundThemeChanged(object sender, EventArgs e)
        {
            A3ServerTool.Properties.Settings.Default.BackgroundTheme = _backgroundTheme;
            A3ServerTool.Properties.Settings.Default.Save();
            ApplyControlBackground();
        }

        private void App_AccentColorChanged(object sender, EventArgs e)
        {
            A3ServerTool.Properties.Settings.Default.AccentColor = _accentColor;
            A3ServerTool.Properties.Settings.Default.Save();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptionHandler.Instance.ShowMessage(e.Exception);
            e.Handled = true;
        }

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                    case "de-DE":
                        dict.Source = new Uri(string.Format("Resources/Lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/Lang.xaml", UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDictionary = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Lang.")
                                              select d).First();
                if (oldDictionary != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldDictionary);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
                    Application.Current.Resources.MergedDictionaries.Insert(index, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                ApplyDot();
                LanguageChanged(Application.Current, EventArgs.Empty);
            }
        }

        public static string BackgroundTheme
        {
            get
            {
                return _backgroundTheme;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (Equals(_backgroundTheme, value))
                {
                    return;
                }

                _backgroundTheme = value;
                
                // ThemeManager.ChangeAppStyle(Application.Current,
                //             ThemeManager.GetAccent(A3ServerTool.Properties.Settings.Default.AccentColor),
                //             ThemeManager.GetAppTheme(value));
                BackgroundThemeChanged(Application.Current, EventArgs.Empty);
            }
        }
        private static string _backgroundTheme;

        public static string AccentColor
        {
            get
            {
                return _accentColor;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if(Equals(_accentColor, value))
                {
                    return;
                }

                _accentColor = value;

                //ThemeManager.Current.ChangeThemeColorScheme(Application.Current)

                ThemeManager.Current.ChangeTheme(Application.Current,
                    A3ServerTool.Properties.Settings.Default.BackgroundTheme);
                
                // ThemeManager.ChangeAppStyle(Application.Current,
                //             ThemeManager.GetAccent(value),
                //             ThemeManager.GetAppTheme(A3ServerTool.Properties.Settings.Default.BackgroundTheme));
                AccentColorChanged(Application.Current, EventArgs.Empty);
            }
        }
        private static string _accentColor;

        protected override void OnStartup(StartupEventArgs e)
        {
            //ThemeManager.Current.ChangeTheme(Application.Current, A3ServerTool.Properties.Settings.Default.BackgroundTheme);
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Olive");
            // ThemeManager.ChangeAppStyle(Application.Current,
            //     ThemeManager.GetAccent(A3ServerTool.Properties.Settings.Default.AccentColor),
            //     ThemeManager.GetAppTheme(A3ServerTool.Properties.Settings.Default.BackgroundTheme));
            ApplyControlBackground();

            base.OnStartup(e);

            Languages.Clear();
            Languages.Add(new CultureInfo("en-US"));
            Languages.Add(new CultureInfo("de-DE"));
            Languages.Add(new CultureInfo("ru-RU"));

            LanguageChanged += App_LanguageChanged;
            BackgroundThemeChanged += App_BackgroundThemeChanged;
            AccentColorChanged += App_AccentColorChanged;
            Language = A3ServerTool.Properties.Settings.Default.Language;
            Bindings.Register();
        }

        private void ApplyControlBackground()
        {
            var dictionary = new ResourceDictionary();

            if (A3ServerTool.Properties.Settings.Default.BackgroundTheme.Contains("Light"))
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
