//  <copyright file="IAuditingConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.Auditing
{
    /// <summary>
    /// Used to configure auditing.
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing system.
        /// Default: true. Set false to completely disable it.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Set true to enable saving audit logs if current user is not logged in.
        /// Default: false.
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// List of selectors to select classes/interfaces which should be audited as default.
        /// </summary>
        IAuditingSelectorList Selectors { get; }

        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        List<Type> IgnoredTypes { get; }
    }
}