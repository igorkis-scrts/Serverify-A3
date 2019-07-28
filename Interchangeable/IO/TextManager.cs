using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Interchangeable.IO
{
    /// <summary>
    /// Provides helper methods to handle 
    /// </summary>
    public static class TextManager
    {
        private const int BufferSize = 128;

        /// <summary>
        /// Read text file
        /// </summary>
        /// <returns>Single string with entire file content</returns>
        public static string ReadFileAsWhole(FileInfo file)
        {
            if (file == null) return null;

            return File.ReadAllText(file.FullName);
        }

        /// <summary>
        /// Read text file line by line.
        /// </summary>
        /// <returns>List of strings</returns>
        public static List<string> ReadFileLineByLine(FileInfo file)
        {
            if (file == null || !file.Exists) return new List<string>();

            var result = new List<string>();

            using (var fileStream = File.OpenRead(file.FullName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
