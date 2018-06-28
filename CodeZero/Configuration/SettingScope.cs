//  <copyright file="SettingScope.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Configuration
{
    /// <summary>
    /// Defines scope of a setting.
    /// </summary>
    [Flags]
    public enum SettingScopes
    {
        /// <summary>
        /// Represents a setting that can be configured/changed for the application level.
        /// </summary>
        Application = 1,

        /// <summary>
        /// Represents a setting that can be configured/changed for each Tenant.
        /// This is reserved
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// Represents a setting that can be configured/changed for each User.
        /// </summary>
        User = 4,

        /// <summary>
        /// Represents a setting that can be configured/changed for all levels
        /// </summary>
        All = Application | Tenant | User
    }
}