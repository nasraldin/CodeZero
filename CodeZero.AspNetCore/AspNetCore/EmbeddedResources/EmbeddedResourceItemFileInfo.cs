//  <copyright file="EmbeddedResourceItemFileInfo.cs" project="CodeZero.AspNetCore" solution="CodeZero">
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
using Microsoft.Extensions.FileProviders;

namespace CodeZero.AspNetCore.EmbeddedResources
{
    public class EmbeddedResourceItemFileInfo : IFileInfo
    {
        public bool Exists => true;

        public long Length => _resourceItem.Content.Length;

        public string PhysicalPath => null;

        public string Name { get; }

        public DateTimeOffset LastModified => _resourceItem.LastModifiedUtc;

        public bool IsDirectory => false;
        
        private readonly EmbeddedResourceItem _resourceItem;
        
        public EmbeddedResourceItemFileInfo(EmbeddedResourceItem resourceItem, string name)
        {
            _resourceItem = resourceItem;
            Name = name;
        }

        public Stream CreateReadStream()
        {
            return new MemoryStream(_resourceItem.Content);
        }
    }
}