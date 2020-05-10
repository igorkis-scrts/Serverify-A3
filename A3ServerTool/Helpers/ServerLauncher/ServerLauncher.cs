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

            var finalParameterString = _serverStringBuilder.FinalArgumentString;
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

            var headlessClientParameterString = finalParameterString + " -client -connect=127.0.0.1 -password=" + profile.ServerConfig.Password;
            for (int i = 0; i < profile.HeadlessClients; i++)
            {
                var headlessClient = new System.Diagnostics.Process
                {
                    StartInfo =
                    {
                        FileName = profile.ExecutablePath,
                        Arguments = headlessClientParameterString,
                        UseShellExecute = false,
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
                    }
                };
                headlessClient.Start();
            }
        }
    }
}
