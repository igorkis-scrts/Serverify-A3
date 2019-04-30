using System;
using System.Collections.Generic;
using System.IO;
namespace Interchangeable.IO
{
    /// <summary>
    /// Represents file to save.
    /// </summary>
    public class SaveDataDto
    {
        /// <summary>
        /// Gets or sets file content in text format.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets extension of file.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets name of file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets array of folders where file will be contained.
        /// </summary>
        /// <remarks> 
        /// Folders will be concated together one by one, path to executable location is already included.
        /// </remarks>
        public List<string> Folders { get; set; } = new List<string>();

        /// <summary>
        /// Gets full path to where file will be written.
        /// </summary>
        public string GetFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(Folders.ToArray()), FileName) + FileExtension;
        }
    }
}