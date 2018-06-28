//  <copyright file="CodeZeroWebConfigurationExtensions.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Web.Configuration;

namespace CodeZero.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure CodeZero Web module.
    /// </summary>
    public static class CodeZeroWebConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero Web module.
        /// </summary>
        public static ICodeZeroWebModuleConfiguration CodeZeroWeb(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroWebModuleConfiguration>();
        }
    }
}