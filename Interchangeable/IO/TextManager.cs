using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable.IO
{
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


        public static List<string> ReadFileLineByLine(FileInfo file)
        {
            if (file == null) return null;

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
