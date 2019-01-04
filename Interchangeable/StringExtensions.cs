using System;
using System.Globalization;

namespace Interchangeable
{
    /// <summary>
    /// String class extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Generates a random string with determined length
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="length">Length of string</param>
        /// <returns>Source string + random string</returns>
        public static string AddRandomString(this string source, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charArray = new char[length];
            var random = new Random();

            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = chars[random.Next(chars.Length)];
            }

            return source + "_" +  new string(charArray);
        }
    }
}
