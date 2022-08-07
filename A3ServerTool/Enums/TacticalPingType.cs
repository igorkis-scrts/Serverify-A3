namespace A3ServerTool.Enums;

/// <summary>
/// Represents tactical ping indication type.
/// </summary>
public enum TacticalPingType
{
    [Description("Hide")]
    Hide = 0,

    [Description("Show on screen")]
    OnlyScreen = 1,

    [Description("Show on map")]
    OnlyMap = 2,

    [Description("Both screen and map")]
    Both = 3
}
