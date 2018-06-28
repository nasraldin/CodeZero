//  <copyright file="EmbeddedResourceRouteHandler.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Web;
using System.Web.Routing;

namespace CodeZero.Web.Mvc.Resources.Embedded.Handlers
{
    [Obsolete]
    internal class EmbeddedResourceRouteHandler : IRouteHandler
    {
        private readonly string _rootPath;

        public EmbeddedResourceRouteHandler(string rootPath)
        {
            _rootPath = rootPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new EmbeddedResourceHttpHandler(_rootPath, requestContext.RouteData);
        }
    }
}