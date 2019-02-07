using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Models
{
    /// <summary>
    /// Enum for ServerSettings properties - it will help to determine server setting environment 
    /// - config file, argument for executable et cetera
    /// </summary>
    public enum SettingSourceType
    {
        None = 0,
        Argument = 1,
        Profile = 2
    }
}
