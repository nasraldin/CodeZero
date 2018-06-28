//  <copyright file="CodeZeroQuartzConfiguration.cs" project="CodeZero.Quartz" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Quartz;
using Quartz.Impl;

namespace CodeZero.Quartz.Configuration
{
    public class CodeZeroQuartzConfiguration : ICodeZeroQuartzConfiguration
    {
        public IScheduler Scheduler => StdSchedulerFactory.GetDefaultScheduler().Result;
    }
}