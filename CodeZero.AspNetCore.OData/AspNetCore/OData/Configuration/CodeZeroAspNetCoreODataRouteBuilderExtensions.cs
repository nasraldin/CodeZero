//  <copyright file="CodeZeroAspNetCoreODataRouteBuilderExtensions.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CodeZero.AspNetCore.OData.Configuration
{
    public static class CodeZeroAspNetCoreODataRouteBuilderExtensions
    {
        public static void MapODataServiceRoute(this IRouteBuilder routes, IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetService<ICodeZeroAspNetCoreODataModuleConfiguration>();

            configuration.MapAction(routes);
        }
    }
}
