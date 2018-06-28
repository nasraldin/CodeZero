//  <copyright file="IEventBusConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Events.Bus;

namespace CodeZero.Configuration.Startup
{
    /// <summary>
    /// Used to configure <see cref="IEventBus"/>.
    /// </summary>
    public interface IEventBusConfiguration
    {
        /// <summary>
        /// True, to use <see cref="EventBus.Default"/>.
        /// False, to create per <see cref="IIocManager"/>.
        /// This is generally set to true. But, for unit tests,
        /// it can be set to false.
        /// Default: true.
        /// </summary>
        bool UseDefaultEventBus { get; set; }
    }
}