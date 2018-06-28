//  <copyright file="CodeZeroHangfireModuleConfigurationExtensions.cs" project="CodeZero.HangFire" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.BackgroundJobs;
using CodeZero.Configuration.Startup;

namespace CodeZero.Hangfire.Configuration
{
    public static class CodeZeroHangfireConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero Hangfire module.
        /// </summary>
        public static ICodeZeroHangfireConfiguration CodeZeroHangfire(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroHangfireConfiguration>();
        }

        /// <summary>
        /// Configures to use Hangfire for background job management.
        /// </summary>
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration, Action<ICodeZeroHangfireConfiguration> configureAction)
        {
            backgroundJobConfiguration.CodeZeroConfiguration.ReplaceService<IBackgroundJobManager, HangfireBackgroundJobManager>();
            configureAction(backgroundJobConfiguration.CodeZeroConfiguration.Modules.CodeZeroHangfire());
        }
    }
}