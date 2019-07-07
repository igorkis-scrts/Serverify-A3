using A3ServerTool.Helpers;
using A3ServerTool.Models;
using A3ServerTool.Models.Config;
using A3ServerTool.Storage;
using GalaSoft.MvvmLight.Ioc;

namespace A3ServerTool
{
    /// <summary>
    /// Represents non-VM bindings in SimpleIoc container.
    /// </summary>
    public static class Bindings
    {
        /// <summary>
        /// Registers types.
        /// </summary>
        public static void Register()
        {
            SimpleIoc.Default.Register<IDao<Profile>, JsonProfileDao>();
            SimpleIoc.Default.Register<IConfigDao<BasicConfig>,BasicConfigDao>();
            SimpleIoc.Default.Register<IConfigDao<ServerConfig>, ServerConfigDao>();
            SimpleIoc.Default.Register<IConfigDao<ArmaProfile>, ArmaProfileDao>();

            SimpleIoc.Default.Register<IProfileDirector, ProfileDirector>();
            SimpleIoc.Default.Register<IServerLauncher, ServerLauncher>();
            SimpleIoc.Default.Register<IMissionDirector, MissionDirector>();
            SimpleIoc.Default.Register<IDao<Mission>, MissionDao>();
        }
    }
}
