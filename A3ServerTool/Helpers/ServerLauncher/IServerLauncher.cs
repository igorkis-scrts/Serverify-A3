using A3ServerTool.Models;

namespace A3ServerTool.Helpers.ServerLauncher
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
