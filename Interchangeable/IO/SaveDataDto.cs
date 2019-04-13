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
        public string Content { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }

        public List<string> Folders { get; set; } = new List<string>();

        public string GetFullPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(Folders.ToArray()), FileName) + FileExtension;
        }
    }
}