//  <copyright file="ModuleZeroAspNetCoreConfigurationExtensions.cs" project="CodeZero.Identity.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.Identity.AspNetCore
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class IdentityAspNetCoreConfigurationExtensions
    {
        /// <summary>
        /// Configures CodeZero Zero AspNetCore module.
        /// </summary>
        /// <returns></returns>
        public static ICodeZeroIdentityAspNetCoreConfiguration ZeroAspNetCore(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.CodeZeroConfiguration.Get<ICodeZeroIdentityAspNetCoreConfiguration>();
        }
    }
}