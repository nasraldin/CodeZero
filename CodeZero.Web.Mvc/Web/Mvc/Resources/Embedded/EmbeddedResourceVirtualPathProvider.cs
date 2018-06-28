//  <copyright file="EmbeddedResourceVirtualPathProvider.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.Resources.Embedded;

namespace CodeZero.Web.Mvc.Resources.Embedded
{
    public class EmbeddedResourceVirtualPathProvider : VirtualPathProvider, ITransientDependency
    {
        private readonly IEmbeddedResourceManager _embeddedResourceManager;

        public EmbeddedResourceVirtualPathProvider(IEmbeddedResourceManager embeddedResourceManager)
        {
            _embeddedResourceManager = embeddedResourceManager;
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            var resource = GetResource(virtualPath);
            if (resource != null)
            {
                return new EmbeddedResourceItemCacheDependency(resource);
            }

            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override bool FileExists(string virtualPath)
        {
            if (base.FileExists(virtualPath))
            {
                return true;
            }

            return GetResource(virtualPath) != null;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (Previous != null && base.FileExists(virtualPath))
            {
                return Previous.GetFile(virtualPath);
            }
            
            var resource = GetResource(virtualPath);
            if (resource != null)
            {
                return new EmbeddedResourceItemVirtualFile(virtualPath, resource);
            }

            return base.GetFile(virtualPath);
        }

        private EmbeddedResourceItem GetResource(string virtualPath)
        {
            return _embeddedResourceManager.GetResource(VirtualPathUtility.ToAppRelative(virtualPath).RemovePreFix("~"));
        }
    }
}