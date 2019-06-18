using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Represents values for some three-state difficulty parameters.
    /// </summary>
    public enum ThreeStateType
    {
        [Description("0")]
        Hide = 0,

        [Description("1")]
        Limited = 1,

        [Description("2")]
        Show = 2
    }
}
