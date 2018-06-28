//  <copyright file="EmbeddedResourceItemVirtualFile.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.IO;
using System.Web.Hosting;
using CodeZero.Resources.Embedded;

namespace CodeZero.Web.Mvc.Resources.Embedded
{
    public class EmbeddedResourceItemVirtualFile : VirtualFile
    {
        private readonly EmbeddedResourceItem _resource;

        public EmbeddedResourceItemVirtualFile(string virtualPath, EmbeddedResourceItem resource)
            : base(virtualPath)
        {
            _resource = resource;
        }

        public override Stream Open()
        {
            return new MemoryStream(_resource.Content);
        }
    }
}