using System.ComponentModel;

namespace A3ServerTool.Enums
{
    /// <summary>
    /// Represents game difficulty.
    /// </summary>
    public enum DifficultyType
    {
        [Description("")]
        None = 0,

        [Description("Recruit")]
        Recruit = 1,

        [Description("Regular")]
        Regular = 2,

        [Description("Veteran")]
        Veteran = 3,

        [Description("Custom")]
        Custom = 4
    }
}
