//  <copyright file="ICodeZeroAspNetCoreODataModuleConfiguration.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Routing;

namespace CodeZero.AspNetCore.OData.Configuration
{
    /// <summary>
    /// Used to configure CodeZero.AspNetCore.OData module.
    /// </summary>
    public interface ICodeZeroAspNetCoreODataModuleConfiguration
    {
        /// <summary>
        /// Gets ODataConventionModelBuilder.
        /// </summary>
        ODataConventionModelBuilder ODataModelBuilder { get; set; }

        /// <summary>
        /// Allows overriding OData mapping.
        /// </summary>
        Action<IRouteBuilder> MapAction { get; set; }
    }
}
