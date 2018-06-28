//  <copyright file="CodeZeroAspNetCoreODataConfigurationExtensions.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.AspNetCore.OData.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure CodeZero.AspNetCore.OData module.
    /// </summary>
    public static class CodeZeroAspNetCoreODataConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero.AspNetCore.OData module.
        /// </summary>
        public static ICodeZeroAspNetCoreODataModuleConfiguration CodeZeroAspNetCoreOData(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroAspNetCoreODataModuleConfiguration>();
        }
    }
}
