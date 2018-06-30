//  <copyright file="CodeZeroWebApiODataModuleConfiguration.cs" project="CodeZero.Web.Api.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System;

namespace CodeZero.WebApi.OData.Configuration
{
    internal class CodeZeroWebApiODataModuleConfiguration : ICodeZeroWebApiODataModuleConfiguration
    {
        public ODataConventionModelBuilder ODataModelBuilder { get; set; }

        public Action<ICodeZeroStartupConfiguration> MapAction { get; set; }

        public CodeZeroWebApiODataModuleConfiguration()
        {
            ODataModelBuilder = new ODataConventionModelBuilder();

            MapAction = configuration =>
            {
                configuration.Modules.CodeZeroWebApi().HttpConfiguration.MapODataServiceRoute(
                    routeName: "ODataRoute",
                    routePrefix: "odata",
                    model: configuration.Modules.CodeZeroWebApiOData().ODataModelBuilder.GetEdmModel()
                );
            };
        }
    }
}