//  <copyright file="EmbeddedResourceItem.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace CodeZero.Resources.Embedded
{
    /// <summary>
    /// Stores needed information of an embedded resource.
    /// </summary>
    public class EmbeddedResourceItem
    {
        /// <summary>
        /// File name including extension.
        /// </summary>
        public string FileName { get; }

        [CanBeNull]
        public string FileExtension { get; }

        /// <summary>
        /// Content of the resource file.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// The assembly that contains the resource.
        /// </summary>
        public Assembly Assembly { get; set; }

        public DateTime LastModifiedUtc { get; }

        internal EmbeddedResourceItem(string fileName, byte[] content, Assembly assembly)
        {
            FileName = fileName;
            Content = content;
            Assembly = assembly;
            FileExtension = CalculateFileExtension(FileName);
            LastModifiedUtc = Assembly.Location != null
                ? new FileInfo(Assembly.Location).LastWriteTimeUtc
                : DateTime.UtcNow;
        }

        private static string CalculateFileExtension(string fileName)
        {
            if (!fileName.Contains("."))
            {
                return null;
            }

            return fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
        }
    }
}