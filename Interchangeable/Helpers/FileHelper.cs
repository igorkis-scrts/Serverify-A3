using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interchangeable.Helpers
{
    /// <summary>
    /// Various methods for I/O things
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Get all specified files from folder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static List<FileInfo> GetSpecificFiles(string path, string fileType)
        {
            //var fileList = new List<FileInfo>();

            //await Task.Run(() =>
            //{
                //fileList = new DirectoryInfo(path)
                //    .GetFiles("*." + fileType, SearchOption.AllDirectories)
                //    .ToList();
            //});

            return new DirectoryInfo(path)
                .GetFiles("*." + fileType, SearchOption.AllDirectories)
                .ToList();
        }
    }
}
