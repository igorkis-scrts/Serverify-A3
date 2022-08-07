namespace A3ServerTool.Attributes;

/// <summary>
/// Provides theme attributes.
/// </summary>
internal sealed class ThemeAttribute : Attribute
{
    /// <summary>
    /// Gets or sets real title of theme.
    /// </summary>
    public string RealTitle { get; set; }

    public ThemeAttribute(string title)
    {
        RealTitle = title;
    }
}