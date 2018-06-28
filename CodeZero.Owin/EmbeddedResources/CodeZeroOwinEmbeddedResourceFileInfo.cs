//  <copyright file="CodeZeroOwinEmbeddedResourceFileInfo.cs" project="CodeZero.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.IO;
using CodeZero.Resources.Embedded;
using Microsoft.Owin.FileSystems;

namespace CodeZero.Owin.EmbeddedResources
{
    public class CodeZeroOwinEmbeddedResourceFileInfo : IFileInfo
    {
        public long Length => _resource.Content.Length;

        public string PhysicalPath => null;

        public string Name => _resource.FileName;

        public DateTime LastModified => _resource.LastModifiedUtc;

        public bool IsDirectory => false;

        private readonly EmbeddedResourceItem _resource;

        public CodeZeroOwinEmbeddedResourceFileInfo(EmbeddedResourceItem resource)
        {
            _resource = resource;
        }

        public Stream CreateReadStream()
        {
            return new MemoryStream(_resource.Content);
        }
    }
}