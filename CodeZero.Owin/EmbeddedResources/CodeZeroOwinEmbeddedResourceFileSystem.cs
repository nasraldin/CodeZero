//  <copyright file="CodeZeroOwinEmbeddedResourceFileSystem.cs" project="CodeZero.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Web;
using CodeZero.Dependency;
using CodeZero.Resources.Embedded;
using CodeZero.Web.Configuration;
using Microsoft.Owin.FileSystems;

namespace CodeZero.Owin.EmbeddedResources
{
    public class CodeZeroOwinEmbeddedResourceFileSystem : IFileSystem, ITransientDependency
    {
        private readonly IEmbeddedResourceManager _embeddedResourceManager;
        private readonly IWebEmbeddedResourcesConfiguration _configuration;
        private readonly IFileSystem _physicalFileSystem;

        public CodeZeroOwinEmbeddedResourceFileSystem(
            IEmbeddedResourceManager embeddedResourceManager,
            IWebEmbeddedResourcesConfiguration configuration,
            string rootFolder)
        {
            _embeddedResourceManager = embeddedResourceManager;
            _configuration = configuration;
            _physicalFileSystem = new PhysicalFileSystem(rootFolder);
        }

        public bool TryGetFileInfo(string subpath, out IFileInfo fileInfo)
        {
            if (_physicalFileSystem.TryGetFileInfo(subpath, out fileInfo))
            {
                return true;
            }

            var resource = _embeddedResourceManager.GetResource(subpath);

            if (resource == null || IsIgnoredFile(resource))
            {
                fileInfo = null;
                return false;
            }

            fileInfo = new CodeZeroOwinEmbeddedResourceFileInfo(resource);
            return true;
        }

        public bool TryGetDirectoryContents(string subpath, out IEnumerable<IFileInfo> contents)
        {
            if (_physicalFileSystem.TryGetDirectoryContents(subpath, out contents))
            {
                return true;
            }

            //TODO: Implement..?

            contents = null;
            return false;
        }

        private bool IsIgnoredFile(EmbeddedResourceItem resource)
        {
            return resource.FileExtension != null && _configuration.IgnoredFileExtensions.Contains(resource.FileExtension);
        }
    }
}