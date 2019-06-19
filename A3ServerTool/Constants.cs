using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool
{
    /// <summary>
    /// Provides various unchangeable (in runtime) values for application - file extensions, relative paths etc.
    /// </summary>
    internal static class Constants
    {
        internal static string RootFolder => AppDomain.CurrentDomain.BaseDirectory;

        internal const string ConfigFileExtension = ".cfg";
        internal const string GameProfileFileExtension = ".Arma3Profile";
        internal const string ServerProfileFileExtension = ".json";
        internal const string MissionFileExtension = ".pbo";

        internal const string BasicConfigFileName = "basic";
        internal const string ServerConfigFileName = "server";
        internal const string GameProfileName = "serverProfile";
        internal const string MissionFolderName = "MPMissions";
    }
}
