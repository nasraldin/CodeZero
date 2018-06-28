//  <copyright file="CodeZeroQuartzConfigurationExtensions.cs" project="CodeZero.Quartz" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;

namespace CodeZero.Quartz.Configuration
{
    public static class CodeZeroQuartzConfigurationExtensions
    {
        /// <summary>
        ///     Used to configure CodeZero Quartz module.
        /// </summary>
        public static ICodeZeroQuartzConfiguration CodeZeroQuartz(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroQuartzConfiguration>();
        }
    }
}
