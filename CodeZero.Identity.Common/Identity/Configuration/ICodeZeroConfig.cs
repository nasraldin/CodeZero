//  <copyright file="ICodeZeroConfig.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Identity.Configuration
{
    /// <summary>
    /// Configuration options for Identity module.
    /// </summary>
    public interface ICodeZeroConfig
    {
        /// <summary>
        /// Gets role management config.
        /// </summary>
        IRoleManagementConfig RoleManagement { get; }

        /// <summary>
        /// Gets user management config.
        /// </summary>
        IUserManagementConfig UserManagement { get; }

        /// <summary>
        /// Gets language management config.
        /// </summary>
        ILanguageManagementConfig LanguageManagement { get; }

        /// <summary>
        /// Gets entity type config.
        /// </summary>
        ICodeZeroEntityTypes EntityTypes { get; }
    }
}