using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    public class ServerLauncher
    {
        //TODO: Builder pattern for server properties
        //TODO: grab every setting and throw it into appropriate place using SettingSource

        //temporary, obviously, this is proof of concept
        public void Run(string port, string name, string path)
        {
            var server = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    FileName = path,
                    Arguments = "-port=" + port,
                    UseShellExecute = false,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized //maybe make this thing as setting in Settings?
                }
            };
            server.Start();
        }
    }
}
