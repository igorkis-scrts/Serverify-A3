using System;
using System.Collections.Generic;
using System.IO;
namespace Interchangeable.IO
{
    /// <summary>
    /// A simple class to send parameters to FileHelper in a packed way
    /// </summary>
    public class SaveDataDto
    {
        /// <summary>
        /// File content in text format
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Extension of file
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Name of file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Array of folders where file will be contained
        /// </summary>
        /// <remarks> 
        /// Folders will be concated together one by one, path to executable location is already included.
        /// </remarks>
        public List<string> Folders { get; set; } = new List<string>();

        /// <summary>
        /// Provides full path to writable file
        /// </summary>
        public string GetFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(Folders.ToArray()), FileName) + FileExtension;
        }
    }
}