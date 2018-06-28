//  <copyright file="EmbeddedResourceItemCacheDependency.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Web.Caching;
using CodeZero.Resources.Embedded;

namespace CodeZero.Web.Mvc.Resources.Embedded
{
    public class EmbeddedResourceItemCacheDependency : CacheDependency
    {
        public EmbeddedResourceItemCacheDependency(EmbeddedResourceItem resource)
        {
            SetUtcLastModified(resource.LastModifiedUtc);
        }
    }
}