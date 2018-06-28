//  <copyright file="ICodeZeroWebApiODataModuleConfiguration.cs" project="CodeZero.Web.Api.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Web.OData.Builder;
using CodeZero.Configuration.Startup;

namespace CodeZero.WebApi.OData.Configuration
{
    /// <summary>
    /// Used to configure CodeZero.Web.Api.OData module.
    /// </summary>
    public interface ICodeZeroWebApiODataModuleConfiguration
    {
        /// <summary>
        /// Gets ODataConventionModelBuilder.
        /// </summary>
        ODataConventionModelBuilder ODataModelBuilder { get; set; }

        /// <summary>
        /// Allows overriding OData mapping.
        /// </summary>
        Action<ICodeZeroStartupConfiguration> MapAction { get; set; }
    }
}