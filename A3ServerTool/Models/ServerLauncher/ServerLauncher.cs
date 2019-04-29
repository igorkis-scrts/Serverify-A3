using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <inheritdoc/>
    public class ServerLauncher : IServerLauncher
    {
        /// <inheritdoc/>
        public void Run(Profile profile)
        {
            if (profile == null) return;

            //TODO: grab cmd arguments
            //TODO: grab basic.cfg path
            //TODO: grab server.cfg path
            //TODO: grab profilePath

            var server = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = profile.ArgumentSettings.ExecutablePath,
                    Arguments = AppendProfileSettings(profile),
                    UseShellExecute = false,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
                }
            };
            server.Start();
        }

        private string AppendProfileSettings(Profile profile)
        {
            var builder = new StringBuilder();

            builder.Append("-cfg=" + profile.BasicConfigString);

            var tt = builder.ToString();

            return tt;
        }
    }
}
