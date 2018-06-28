//  <copyright file="CodeZeroAspNetCoreConfigurationExtensions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.AspNetCore.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure CodeZero ASP.NET Core module.
    /// </summary>
    public static class CodeZeroAspNetCoreConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero ASP.NET Core module.
        /// </summary>
        public static ICodeZeroAspNetCoreConfiguration CodeZeroAspNetCore(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroAspNetCoreConfiguration>();
        }
    }
}