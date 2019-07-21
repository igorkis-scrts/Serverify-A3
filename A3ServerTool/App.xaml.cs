using A3ServerTool.Enums;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
            A3ServerTool.Properties.Settings.Default.BackgroundTheme = BackgroundTheme.Name;
            A3ServerTool.Properties.Settings.Default.Save();
        }

        private void App_AccentColorChanged(object sender, EventArgs e)
        {
            A3ServerTool.Properties.Settings.Default.AccentColor = AccentColor.Name;
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
                        dict.Source = new Uri(String.Format("Resources/Lang.{0}.xaml", value.Name), UriKind.Relative);
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

        public static AppTheme BackgroundTheme
        {
            get
            {
                return ThemeManager.DetectAppStyle(Application.Current).Item1;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                ThemeManager.ChangeAppTheme(Current, value.Name);

                //ThemeManager.ChangeAppStyle(Application.Current,
                //            ThemeManager.GetAccent(A3ServerTool.Properties.Settings.Default.AccentColor),
                //            ThemeManager.GetAppTheme(value.Name));
                BackgroundThemeChanged(Application.Current, EventArgs.Empty);
            }
        }

        public static Accent AccentColor
        {
            get
            {
                return ThemeManager.GetAccent(A3ServerTool.Properties.Settings.Default.AccentColor);
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                ThemeManager.ChangeAppStyle(Application.Current,
                            ThemeManager.GetAccent(value.Name),
                            BackgroundTheme);
                AccentColorChanged(Application.Current, EventArgs.Empty);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                ThemeManager.GetAccent(A3ServerTool.Properties.Settings.Default.AccentColor),
                ThemeManager.GetAppTheme(A3ServerTool.Properties.Settings.Default.BackgroundTheme));

            base.OnStartup(e);

            Languages.Clear();
            Languages.Add(new CultureInfo("en-US"));
            Languages.Add(new CultureInfo("ru-RU"));

            LanguageChanged += App_LanguageChanged;
            BackgroundThemeChanged += App_BackgroundThemeChanged;
            AccentColorChanged += App_AccentColorChanged;
            Language = A3ServerTool.Properties.Settings.Default.Language;
            Bindings.Register();
        }

        /// <summary>
        /// Applies the dot instead of comma for current culture.
        /// </summary>
        private static void ApplyDot()
        {
            var customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //CultureInfo.CurrentCulture = customCulture;
        }
    }
}
