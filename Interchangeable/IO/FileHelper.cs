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
    public sealed class FileHelper
    {
        private static FileHelper _instance;
        //private static string RootFolder => AppDomain.CurrentDomain.BaseDirectory;

        private FileHelper() {}

        public static FileHelper GetInstance()
        {
            return _instance ?? (_instance = new FileHelper());
        }

        /// <summary>
        /// Get files from directory
        /// </summary>
        public static List<FileInfo> GetAllFiles(DirectoryInfo directory)
        {
            if(directory == null) return new List<FileInfo>();

            return directory
                .GetFiles("*", SearchOption.TopDirectoryOnly)
                .ToList();
        }

        /// <summary>
        /// Get all folders that path contains
        /// </summary>
        public static List<DirectoryInfo> GetFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                return null;
            }

            return new DirectoryInfo(folderPath)
                .GetDirectories()
                .ToList();
        }

        /// <summary>
        /// Get file
        /// </summary>
        public static FileInfo GetFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            return new FileInfo(path);
        }


        /// <summary>
        /// Saves strings to hard drive
        /// </summary>
        public static void Save(SaveDataDto dto)
        {
            if (dto == null) return;

            var path = Path.Combine(dto.Folders.Select(x => x).ToArray());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(dto.GetFullPath(), dto.Content);
        }

        /// <summary>
        /// Deletes a file from hard drive
        /// </summary>
        public static void Delete(string fileName, string folderName = null)
        {
            var path = folderName != null
                ? Path.Combine(folderName, fileName)
                : fileName;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                throw new FileNotFoundException("File is not exists.");
            }
        }

        /// <summary>
        /// Deletes a folder with all it's content from hard drive
        /// </summary>
        public static void DeleteFolder(SaveDataDto dto)
        {
            if (dto == null) return;

            var path = Path.Combine(dto.Folders.ToArray());
            var directoryInfo = new DirectoryInfo(path).GetDirectories();

            if (Directory.Exists(path))
            {
                foreach (DirectoryInfo directory in directoryInfo)
                {
                    directory.Delete(true);
                }

                foreach (var file in GetAllFiles(new DirectoryInfo(path)))
                {
                    file.Delete();
                }

                 Directory.Delete(path);
            }
            else
            {
                throw new DirectoryNotFoundException("Directory not found!");
            }
        }

        /// <summary>
        /// Creates new folder
        /// </summary>
        public static void CreateFolder(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// Checks if folder exists in application location
        /// </summary>
        public static bool CheckFolderExistence(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        /// <summary>
        /// Checks if file exists in application location
        /// </summary>
        public static bool CheckFileExistence(string path)
        {
            return File.Exists(path);
        }
    }
}
