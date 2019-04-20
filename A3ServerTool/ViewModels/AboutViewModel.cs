﻿using System;
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
    /// About view model
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Avatar = GetImage();
            });
        }

       

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
        /// Github avatar
        /// </summary>
        public ImageSource Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                RaisePropertyChanged();
            }
        }
        private ImageSource _avatar;

        /// <summary>
        /// Get current github avatar from URL
        /// </summary>
        /// <returns>BitmapImage object</returns>
        private BitmapImage GetImage()
        {
            var bitmapImage = new BitmapImage();

            //TODO: Get rid of hardcoded link?
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new DownloadHelper().DownloadFile("https://avatars2.githubusercontent.com/u/6746043?s=460&v=4");
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
