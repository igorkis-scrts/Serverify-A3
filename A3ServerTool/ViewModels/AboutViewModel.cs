using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using Interchangeable.Helpers;

namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// Represents about viewmodel.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the application title.
        /// </summary>
        public string AppTitle { get; } = Properties.Settings.Default.AppTitle;

        /// <summary>
        /// Current application version
        /// </summary>
        public string CurrentVersion
        {
            get => _currentVersion;
            set
            {
                _currentVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _currentVersion;

        /// <summary>
        /// Gets the current date time.
        /// </summary>
        public string CurrentDateTime { get; } = DateTime.Today.Year.ToString();

        public AboutViewModel()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            });
        }
    }
}
