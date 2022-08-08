namespace A3ServerTool.Enums;

/// <summary>
/// Represents visibility state for some difficulty parameters.
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
