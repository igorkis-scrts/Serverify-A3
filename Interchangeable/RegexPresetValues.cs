using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interchangeable
{
    /// <summary>
    /// Provides instruments to check if input strings are valid to certain predefined RegExes.
    /// </summary>
    public static class RegexPresetValues
    {
        public static Regex OnlyNumeric = new Regex("[^0-9]+");
        public static Regex OnlyNumericWithCommas = new Regex("^[0-9,\\s]*$");
        public static Regex OnlyLettersWithCommas = new Regex("^[a-z,\\s]*$");
        public static Regex OnlyOneOrTwoLimitedToSixWithCommas = new Regex("^[0-1,\\s]{0,6}$");
    }
}
