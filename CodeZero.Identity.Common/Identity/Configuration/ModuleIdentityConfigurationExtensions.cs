//  <copyright file="ModuleIdentityConfigurationExtensions.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.Identity.Configuration
{
    /// <summary>
    /// Extension methods for module Identity configurations.
    /// </summary>
    public static class ModuleIdentityConfigurationExtensions
    {
        /// <summary>
        /// Used to configure module Identity.
        /// </summary>
        /// <param name="moduleConfigurations"></param>
        /// <returns></returns>
        public static ICodeZeroConfig Identity(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.CodeZeroConfiguration.Get<ICodeZeroConfig>();
        }
    }
}