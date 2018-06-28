//  <copyright file="IPermissionManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.MultiTenancy;

namespace CodeZero.Authorization
{
    /// <summary>
    /// Permission manager.
    /// </summary>
    public interface IPermissionManager
    {
        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or throws exception
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        Permission GetPermission(string name);

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or returns null
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        Permission GetPermissionOrNull(string name);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="tenancyFilter">Can be passed false to disable tenancy filter.</param>
        IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="multiTenancySides">Multi-tenancy side to filter</param>
        IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides);
    }
}
