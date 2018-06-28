//  <copyright file="IEntityHistoryConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.EntityHistory
{
    /// <summary>
    /// Used to configure entity history.
    /// </summary>
    public interface IEntityHistoryConfiguration
    {
        /// <summary>
        /// Used to enable/disable entity history system.
        /// Default: true. Set false to completely disable it.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Set true to enable saving entity history if current user is not logged in.
        /// Default: false.
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// List of selectors to select classes/interfaces which should be tracked as default.
        /// </summary>
        IEntityHistorySelectorList Selectors { get; }

        /// <summary>
        /// Ignored types for serialization on entity history tracking.
        /// </summary>
        List<Type> IgnoredTypes { get; }
    }
}
