using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpperCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return input[0].ToString().ToUpper() + string.Join(string.Empty, input.Skip(1));
        }
    }
}
