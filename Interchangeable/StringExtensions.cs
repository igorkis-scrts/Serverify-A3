using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable
{
    /// <summary>
    /// Provides various string extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Uppers first letter in string to upper case.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Same string, but with first letter in upper case.</returns>
        public static string FirstLetterToUpperCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return input[0].ToString().ToUpper() + string.Join(string.Empty, input.Skip(1));
        }

        /// <summary>
        /// Surrounds string the with characters.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="letters">The letters.</param>
        /// <param name="character">The character.</param>
        /// <returns>String surrouned with specified symbols.</returns>
        /// <remarks>There are two optional parameters - input can be surrounded either with string or with character. String has priority above character. </remarks>
        public static string SurroundWithCharacters(this string input, string letters = "", char character = char.MinValue)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            if(!string.IsNullOrWhiteSpace(letters))
            {
                return string.Concat(letters, input, letters);
            }
            if(character != char.MinValue)
            {
                return string.Concat(character, letters, character);
            }

            return input;
        }
    }
}
