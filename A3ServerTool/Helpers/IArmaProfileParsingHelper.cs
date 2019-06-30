using A3ServerTool.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Helpers
{
    /// <summary>
    /// Provides little helper methods to help with parsing .Arma3Profile file
    /// or converting class instance into .Arm3Profile file back.
    /// </summary>
    public interface IArmaProfileParsingHelper
    {
        /// <summary>
        /// Gets the Arma profile.
        /// </summary>
        /// <returns>Game settings that will be applied on the server.</returns>
        /// <param name="properties">Text properties (server.cfg).</param>
        ArmaProfile GetProfile(IEnumerable<string> properties);
    }
}
