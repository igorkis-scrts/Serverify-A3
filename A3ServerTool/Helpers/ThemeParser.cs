using System;
using System.Linq;
using A3ServerTool.Attributes;
using A3ServerTool.Enums;

namespace A3ServerTool.Helpers
{
    public class ThemeParser : IThemeParser
    {
        public string ConvertThemeToRealTitle(ThemeType theme)
        {
            var enumType = typeof(ThemeType);
            var memberInfos = enumType.GetMember(theme.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(ThemeAttribute), false);

            return ((ThemeAttribute) valueAttributes[0]).RealTitle;
        }

        public ThemeType ConvertRealTitleToTheme(string title)
        {
            var enumName = title.Replace(".", string.Empty);
            
            Enum.TryParse(enumName, out ThemeType theme);
            return theme;
        }
    }
}