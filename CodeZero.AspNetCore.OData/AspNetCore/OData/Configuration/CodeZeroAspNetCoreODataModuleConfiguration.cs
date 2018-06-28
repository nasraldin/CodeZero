//  <copyright file="CodeZeroAspNetCoreODataModuleConfiguration.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;

namespace CodeZero.AspNetCore.OData.Configuration
{
    internal class CodeZeroAspNetCoreODataModuleConfiguration : ICodeZeroAspNetCoreODataModuleConfiguration
    {
        public ODataConventionModelBuilder ODataModelBuilder { get; set; }

        public Action<IRouteBuilder> MapAction { get; set; }

        public CodeZeroAspNetCoreODataModuleConfiguration()
        {
            MapAction = routes =>
            {
                routes.MapODataServiceRoute(
                    routeName: "ODataRoute",
                    routePrefix: "odata",
                    model: ODataModelBuilder.GetEdmModel()
                );

                // Workaround: https://github.com/OData/WebApi/issues/1175
                routes.EnableDependencyInjection();
            };
        }
    }
}
