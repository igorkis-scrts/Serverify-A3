using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Provides instruments to launch server with all it's settings.
    /// </summary>
    public interface IServerLauncher
    {
        /// <summary>
        /// Runs server.
        /// </summary>
        /// <param name="profile">Server profile.</param>
        void Run(Profile profile);
    }
}
