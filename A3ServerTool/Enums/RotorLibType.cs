using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Represents different types of RotorLib helicopter flight simulation.
    /// </summary>
    public enum RotorLibType
    {
        [Description("Up to player")]
        Default = 0,

        [Description("Forced Advanced Flight Model")]
        Afm = 1,

        [Description("Forced Simplified Flight Model")]
        Sfm = 2
    }
}
