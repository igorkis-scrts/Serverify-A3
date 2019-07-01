using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Represents distance values for some three-state difficulty parameters.
    /// </summary>
    public enum VisibilityType
    {
        [Description("Hide")]
        Hide = 0,

        [Description("Limited distance")]
        Limited = 1,

        [Description("Always show")]
        Show = 2
    }
}
