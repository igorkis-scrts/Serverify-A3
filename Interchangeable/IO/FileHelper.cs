using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Interchangeable.IO
{
    /// <summary>
    /// Various methods for I/O things
    /// </summary>
    public sealed class FileHelper
    {
        private static FileHelper _instance;

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
            try
            {
                if (directory == null) return new List<FileInfo>();

                return directory
                .GetFiles("*", SearchOption.TopDirectoryOnly)
                .ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return null;
                //sometimes windows blocks files, so we should move on there
            }
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
                throw new FileNotFoundException("File not exists.");
            }
        }

        /// <summary>
        /// Deletes the folder recursively.
        /// </summary>
        /// <param name="path">The path to folder.</param>
        /// <remarks>Sometimes Directory.Delete when directory was blocked by something else.</remarks>
        public static void DeleteFolder(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                return;
            }

            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteFolder(directory);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
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
