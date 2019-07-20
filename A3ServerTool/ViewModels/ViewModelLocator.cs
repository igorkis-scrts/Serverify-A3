/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Coh2StatsParserApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using A3ServerTool.ViewModels.ServerSubViewModels;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;

namespace A3ServerTool.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ServerViewModel Server => ServiceLocator.Current.GetInstance<ServerViewModel>();
        public AboutViewModel About => ServiceLocator.Current.GetInstance<AboutViewModel>();
        public ProfilesViewModel Profiles => ServiceLocator.Current.GetInstance<ProfilesViewModel>();
        public SettingsViewModel Settings => ServiceLocator.Current.GetInstance<SettingsViewModel>();
        public ProfileDialogViewModel ProfileDialog => ServiceLocator.Current.GetInstance<ProfileDialogViewModel>();
        public GeneralViewModel General => ServiceLocator.Current.GetInstance<GeneralViewModel>();
        public NetworkViewModel Network => ServiceLocator.Current.GetInstance<NetworkViewModel>();
        public SecurityViewModel Security => ServiceLocator.Current.GetInstance<SecurityViewModel>();
        public LoggingViewModel Logging => ServiceLocator.Current.GetInstance<LoggingViewModel>();
        public PerfromanceViewModel Performance => ServiceLocator.Current.GetInstance<PerfromanceViewModel>();
        public ModificationsViewModel Modifications => ServiceLocator.Current.GetInstance<ModificationsViewModel>();
        public MissionsViewModel Missions => ServiceLocator.Current.GetInstance<MissionsViewModel>();
        public DifficultyViewModel Difficulty => ServiceLocator.Current.GetInstance<DifficultyViewModel>();

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ProfilesViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<ServerViewModel>();
            SimpleIoc.Default.Register<GeneralViewModel>();
            SimpleIoc.Default.Register<IDialogCoordinator, DialogCoordinator>();
            SimpleIoc.Default.Register<ProfileDialogViewModel>();
            SimpleIoc.Default.Register<NetworkViewModel>();
            SimpleIoc.Default.Register<SecurityViewModel>();
            SimpleIoc.Default.Register<LoggingViewModel>();
            SimpleIoc.Default.Register<PerfromanceViewModel>();
            SimpleIoc.Default.Register<ModificationsViewModel>();
            SimpleIoc.Default.Register<MissionsViewModel>();
            SimpleIoc.Default.Register<DifficultyViewModel>();
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}