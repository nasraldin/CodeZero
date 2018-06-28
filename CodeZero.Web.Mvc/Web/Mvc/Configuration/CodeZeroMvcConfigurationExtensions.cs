//  <copyright file="CodeZeroMvcConfigurationExtensions.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.Web.Mvc.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure CodeZero.Web.Api module.
    /// </summary>
    public static class CodeZeroMvcConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero.Web.Api module.
        /// </summary>
        public static ICodeZeroMvcConfiguration CodeZeroMvc(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroMvcConfiguration>();
        }
    }
}