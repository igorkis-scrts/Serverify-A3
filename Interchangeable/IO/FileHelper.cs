using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Interchangeable.IO
{
    /// <summary>
    /// Various methods for I/O things
    /// </summary>
    /// TODO: Is singleton really necessary here?
    /// TODO: Class rename?
    public class FileHelper
    {
        private static FileHelper _instance;

        private FileHelper() {}

        public static FileHelper GetInstance()
        {
            return _instance ?? (_instance = new FileHelper());
        }

        public static string RootFolder => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Get all specified files from folder
        /// </summary>
        public static List<FileInfo> GetSpecificFiles(string fileExtension, string folderName = null)
        {
            var path = folderName != null
                ? Path.Combine(RootFolder, folderName)
                : RootFolder;

            return new DirectoryInfo(path)
                .GetFiles("*" + fileExtension, SearchOption.TopDirectoryOnly)
                .ToList();
        }

        /// <summary>
        /// Saves strings to hard drive
        /// </summary>
        public static void Save(string content, string fileName, string fileExtension, string folderName = null)
        {
            if (!Directory.Exists(RootFolder + folderName))
            {
                Directory.CreateDirectory(RootFolder + folderName);
            }

            var path = folderName != null 
                ? Path.Combine(RootFolder, folderName, fileName) 
                : Path.Combine(RootFolder, fileName);

            if (File.Exists(path))
            {
                //TODO: throw new exception about file existance
            }
            else
            {

                File.WriteAllText(path + fileExtension, content);
            }
        }

        /// <summary>
        /// Delete file from hard drive
        /// </summary>
        public static void Delete(string fileName, string folderName = null)
        {
            var path = folderName != null
                ? Path.Combine(RootFolder, folderName, fileName)
                : Path.Combine(RootFolder, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                //TODO: throw new exception about nonexistance
            }
        }
    }
}
