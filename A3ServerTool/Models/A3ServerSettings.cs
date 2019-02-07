using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    public class A3ServerSettings : IServerSettings
    {
        /// <inheritdoc/>
        [SettingSource(SourceType = SettingSourceType.Argument)]
        public string Name { get; set; }

        /// <inheritdoc/>
        [SettingSource(SourceType = SettingSourceType.Argument)]
        public string Port { get; set; }

        /// <inheritdoc/>
        [SettingSource(SourceType = SettingSourceType.None)]
        public string ExecutablePath { get; set; }
    }
}
