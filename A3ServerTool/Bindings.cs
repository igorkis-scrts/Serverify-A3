using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A3ServerTool.Models;
using A3ServerTool.Storage;
using GalaSoft.MvvmLight.Ioc;

namespace A3ServerTool
{
    /// <summary>
    /// Separate class to register types in SimpleIoc container.
    /// </summary>
    public static class Bindings
    {
        /// <summary>
        /// Registers types.
        /// </summary>
        public static void Register()
        {
            SimpleIoc.Default.Register<IDao<Profile>, JsonProfileDao>();
            SimpleIoc.Default.Register<BasicConfigDao>();
            SimpleIoc.Default.Register<IProfileDirector, ProfileDirector>();
            SimpleIoc.Default.Register<IServerLauncher, ServerLauncher>();
        }
    }
}
