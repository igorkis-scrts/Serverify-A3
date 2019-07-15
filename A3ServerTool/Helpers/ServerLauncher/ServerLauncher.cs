using A3ServerTool.Models;
using System.Text;

namespace A3ServerTool.Helpers.ServerLauncher
{
    /// <inheritdoc/>
    public class ServerLauncher : IServerLauncher
    {
        private readonly IServerStringBuilder _serverStringBuilder;

        public ServerLauncher(IServerStringBuilder serverStringBuilder)
        {
            _serverStringBuilder = serverStringBuilder;
        }

        /// <inheritdoc/>
        public void Run(Profile profile)
        {
            if (profile == null) return;

            var finalParameterString = _serverStringBuilder.GetFinalArgumentString(profile);
            var server = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = profile.ExecutablePath,
                    Arguments = finalParameterString,
                    UseShellExecute = false,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
                }
            };
            server.Start();

            //TODO: check if server not starts - show message about server executable
        }
    }
}
