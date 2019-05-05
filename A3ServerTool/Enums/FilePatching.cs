using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Provides representation of allowedFilePatching config property in code.
    /// </summary>
    public enum FilePatching
    {
        [Description("No Clients")]
        NoClients = 0,

        [Description("Headless Clients")]
        HeadlessClients = 1,

        [Description("All Clients")]
        AllClients = 2
    }
}
