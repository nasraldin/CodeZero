//  <copyright file="CodeZeroAspNetCoreODataApplicationBuilderExtensions.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeZero.AspNetCore.OData.Configuration
{
    public static class CodeZeroAspNetCoreODataApplicationBuilderExtensions
    {
        public static void UseOData(this IApplicationBuilder app, Action<ODataConventionModelBuilder> builderAction)
        {
            var configuration = app.ApplicationServices.GetService<ICodeZeroStartupConfiguration>();

            if (configuration.Modules.CodeZeroAspNetCoreOData().ODataModelBuilder == null)
            {
                configuration.Modules.CodeZeroAspNetCoreOData().ODataModelBuilder = new ODataConventionModelBuilder(app.ApplicationServices);
            }

            builderAction(configuration.Modules.CodeZeroAspNetCoreOData().ODataModelBuilder);
        }
    }
}
