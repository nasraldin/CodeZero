using CodeZero.Shared;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeZero.Extensions.IO
{
    /// <summary>
    /// A helper class for Directory operations.
    /// </summary>
    public static class DirectoryHelper
    {
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void DeleteIfExists(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory);
            }
        }

        public static void DeleteIfExists(string directory, bool recursive)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive);
            }
        }

        public static void CreateIfNotExists(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        public static bool IsSubDirectoryOf([NotNull] string parentDirectoryPath, [NotNull] string childDirectoryPath)
        {
            Check.NotNull(parentDirectoryPath, nameof(parentDirectoryPath));
            Check.NotNull(childDirectoryPath, nameof(childDirectoryPath));

            return IsSubDirectoryOf(
                new DirectoryInfo(parentDirectoryPath),
                new DirectoryInfo(childDirectoryPath)
            );
        }

        public static bool IsSubDirectoryOf([NotNull] DirectoryInfo parentDirectory,
            [NotNull] DirectoryInfo childDirectory)
        {
            Check.NotNull(parentDirectory, nameof(parentDirectory));
            Check.NotNull(childDirectory, nameof(childDirectory));

            if (parentDirectory.FullName == childDirectory.FullName)
            {
                return true;
            }

            var parentOfChild = childDirectory.Parent;
            if (parentOfChild == null)
            {
                return false;
            }

            return IsSubDirectoryOf(parentDirectory, parentOfChild);
        }

        public static IDisposable ChangeCurrentDirectory(string targetDirectory)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            if (currentDirectory.Equals(targetDirectory, StringComparison.OrdinalIgnoreCase))
            {
                return NullDisposable.Instance;
            }

            Directory.SetCurrentDirectory(targetDirectory);

            return new DisposeAction(() => { Directory.SetCurrentDirectory(currentDirectory); });
        }

        /// <summary>
        /// Gets the total size of a specified folder. It can also check sizes of subdirectory under it as a parameter.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="bIncludeSub"></param>
        /// <returns></returns>
        public static long FolderSize(this DirectoryInfo dir, bool bIncludeSub)
        {
            long totalFolderSize = 0;

            if (!dir.Exists) return 0;

            var files = from f in dir.GetFiles()
                        select f;
            foreach (var file in files) totalFolderSize += file.Length;

            if (bIncludeSub)
            {
                var subDirs = from d in dir.GetDirectories()
                              select d;
                foreach (var subDir in subDirs) totalFolderSize += FolderSize(subDir, true);
            }

            return totalFolderSize;
        }

        /// <summary>
        /// Get all files in a specified directory using. Doesn't include sub-directory files.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<string> ListFiles(this string folderPath)
        {
            if (!Directory.Exists(folderPath)) return null;
            return (from f in Directory.GetFiles(folderPath) select Path.GetFileName(f)).ToList();
        }

        /// <summary>
        /// Delete all files found on the specified folder with a given file extension.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="ext"></param>
        public static void DeleteFiles(this string folderPath, string ext)
        {
            string mask = "*." + ext;

            try
            {
                string[] fileList = Directory.GetFiles(folderPath, mask);

                foreach (string file in fileList)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.Delete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Deleting file. Reason: {0}", ex);
            }
        }
    }
}