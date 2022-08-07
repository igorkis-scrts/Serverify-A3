namespace A3ServerTool.Helpers;

/// <summary>
/// Converts theme to real title and vice versa.
/// </summary>
public interface IThemeParser
{
    string ConvertThemeToRealTitle(ThemeType theme);

    ThemeType ConvertRealTitleToTheme(string title);
}