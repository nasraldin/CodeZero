using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CodeZero.Shared.Extensions.Reflection
{
    public static class AssemblyExtensions
    {
        public static string GetFileVersion(this Assembly assembly)
        {
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }

        /// <summary>
        /// Gets directory path of given assembly or returns null if can not find.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static string GetDirectoryPathOrNull(this Assembly assembly)
        {
            var location = assembly.Location;
            if (location == null)
            {
                return null;
            }

            var directory = new FileInfo(location).Directory;
            if (directory == null)
            {
                return null;
            }

            return directory.FullName;
        }
    }
}
