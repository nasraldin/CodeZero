//  <copyright file="CodeZeroUserManagerExtensions.cs" project="CodeZero.Identity" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Authorization.Roles;
using CodeZero.Threading;

namespace CodeZero.Authorization.Users
{
    /// <summary>
    /// Extension methods for <see cref="CodeZeroUserManager{TRole,TUser}"/>.
    /// </summary>
    public static class CodeZeroUserManagerExtensions
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="manager">User manager</param>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public static bool IsGranted<TRole, TUser>(CodeZeroUserManager<TRole, TUser> manager, long userId, string permissionName)
            where TRole : CodeZeroRole<TUser>, new()
            where TUser : CodeZeroUser<TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            return AsyncHelper.RunSync(() => manager.IsGrantedAsync(userId, permissionName));
        }

        //public static CodeZeroUserManager<TRole, TUser> Login<TRole, TUser>(CodeZeroUserManager<TRole, TUser> manager, string userNameOrEmailAddress, string plainPassword, string tenancyName = null)
        //    where TRole : CodeZeroRole<TUser>, new()
        //    where TUser : CodeZeroUser<TUser>
        //{
        //    if (manager == null)
        //    {
        //        throw new ArgumentNullException(nameof(manager));
        //    }

        //    return AsyncHelper.RunSync(() => manager.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName));
        //}
    }
}