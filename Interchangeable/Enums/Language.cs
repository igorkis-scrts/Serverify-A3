using System.ComponentModel;
using Interchangeable;
using System.Globalization;

namespace Interchangeable.Enums
{
    /// <summary>
    /// Represents language types.
    /// </summary>
    public enum Language
    {
        [Description("")]
        None = 0,

        [Description("English")]
        [RepresentedCultureInfo(Culture = "en-US")]
        English = 1,

        [RepresentedCultureInfo(Culture = "ru-RU")]
        [Description("Русский")]
        Russian = 2
    }
}
