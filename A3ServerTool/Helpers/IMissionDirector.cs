using A3ServerTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers
{
    /// <summary>
    /// Represents methods that works with missions stored in server.cfg.
    /// </summary>
    public interface IMissionDirector
    {
        /// <summary>
        /// Gets the missions.
        /// </summary>
        /// <returns>List of missions that will be played on server.</returns>
        /// <param name="properties">Text properties (server.cfg).</param>
        /// <param name="folderPath">Path to Arma 3 installation.</param>
        IList<Mission> GetMissions(IEnumerable<string> properties, string folderPath);

        /// <summary>
        /// Saves the missions into server.cfg.
        /// </summary>
        /// <param name="missions">Missions specified somewhere in the app.</param>
        void SaveMissions(IEnumerable<Mission> missions);
    }
}
