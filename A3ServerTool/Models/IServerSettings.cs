using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    public interface IServerSettings
    {
        /// <summary>
        /// Server name (in browser)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Server port
        /// </summary>
        string Port { get; set; }
    }
}
