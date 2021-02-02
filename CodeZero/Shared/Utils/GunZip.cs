using CodeZero.Shared;
using System;
using System.IO;
using System.IO.Compression;

namespace CodeZero.Extensions.Utils
{
    /// <summary>
    /// Compress files into .gz extension and decompress files from .gz extension
    /// </summary>
    public static class GunZip
    {
        /// <summary>
        /// Compress file into GunZip format in same location with .gz extension
        /// </summary>
        /// <param name="filePath">Path of original file</param>
        /// <returns>Compress file path</returns>
        public static string Compress(string filePath)
        {
            Check.CheckNullOrWhiteSpace(filePath, "Compress(filePath)");

            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                throw new ArgumentException(string.Format("File doesn't exists: '{0}'", filePath));

            if (fileInfo.Extension == ".gz")
                throw new ArgumentException(string.Format("File is already in compress format: '{0}'", filePath));

            var compressFilePath = filePath + ".gz";
            using var inputStream = fileInfo.OpenRead();
            using var outputStream = File.OpenWrite(compressFilePath);
            using var compressStream = new GZipStream(outputStream, CompressionMode.Compress);
            inputStream.CopyTo(compressStream);
            return compressFilePath;
        }

        /// <summary>
        /// Decompress file with .gz extension in same location
        /// </summary>
        /// <param name="compressFilePath">Compress GunZip file path</param>
        /// <returns>Decompressed file path</returns>
        public static string Decompress(string compressFilePath)
        {
            Check.CheckNullOrWhiteSpace(compressFilePath, "Compress(compressFilePath)");

            var compressFileInfo = new FileInfo(compressFilePath);
            if (!compressFileInfo.Exists)
                throw new ArgumentException(string.Format("Compress file doesn't exists: '{0}'", compressFilePath));

            if (compressFileInfo.Extension != ".gz")
                throw new ArgumentException(string.Format("Compress file extension should be .gz: '{0}'", compressFilePath));

            var filePath = Path.GetFileNameWithoutExtension(compressFilePath);
            using var inputStream = compressFileInfo.OpenRead();
            using var outputStream = File.OpenWrite(filePath);
            using var decompressStream = new GZipStream(outputStream, CompressionMode.Decompress);
            inputStream.CopyTo(decompressStream);
            return filePath;
        }
    }
}
