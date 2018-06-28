//  <copyright file="EmbeddedResourceHttpHandler.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using CodeZero.Extensions;

namespace CodeZero.Web.Mvc.Resources.Embedded.Handlers
{
    [Obsolete]
    internal class EmbeddedResourceHttpHandler : IHttpHandler
    {
        private static readonly HashSet<string> IgnoredFileExtensions = new HashSet<string>
        {
            ".cshtml",
            ".config"
        };

        private readonly string _rootPath;
        private readonly RouteData _routeData;

        public EmbeddedResourceHttpHandler(string rootPath, RouteData routeData)
        {
            _rootPath = rootPath;
            _routeData = routeData;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();

            var fileName = _routeData.Values["pathInfo"] as string;
            if (fileName == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            var resource = WebResourceHelper.GetEmbeddedResource(_rootPath.EnsureEndsWith('/') + fileName);
            if (resource == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            if (resource.FileExtension != null && IgnoredFileExtensions.Contains(resource.FileExtension.EnsureStartsWith('.')))
            {
                context.Response.StatusCode = 404;
                return;
            }
            
            context.Response.ContentType = MimeMapping.GetMimeMapping(fileName);
            context.Response.OutputStream.Write(resource.Content, 0, resource.Content.Length);
        }

        public bool IsReusable => false;
    }
}