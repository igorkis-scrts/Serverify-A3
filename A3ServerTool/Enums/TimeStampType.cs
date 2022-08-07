namespace A3ServerTool.Enums;

/// <summary>
/// Represents time stamp format type in the server logs.
/// </summary>
public enum TimeStampType
{
    [Description("None")]
    None = 0,

    [Description("Short")]
    Short = 1,

    [Description("Full")]
    Full = 2
}
