using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Represents visibility states for some three-state difficulty parameters.
    /// </summary>
    public enum IndicationType
    {
        [Description("Hide")]
        Hide = 0,

        [Description("Fade out")]
        Fadeout = 1,

        [Description("Show")]
        Show = 2
    }
}
