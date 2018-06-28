//  <copyright file="PermissionFinder.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CodeZero.Authorization
{
    /// <summary>
    /// This class is used to get permissions out of the system.
    /// Normally, you should inject and use <see cref="IPermissionManager"/> and use it.
    /// This class can be used in database migrations or in unit tests where CodeZero is not initialized.
    /// </summary>
    public static class PermissionFinder
    {
        /// <summary>
        /// Collects and gets all permissions in given providers.
        /// This method can be used to get permissions in database migrations or in unit tests where CodeZero is not initialized.
        /// Otherwise, use <see cref="IPermissionManager.GetAllPermissions(bool)"/> method.
        /// 
        /// </summary>
        /// <param name="authorizationProviders">Authorization providers</param>
        /// <returns>List of permissions</returns>
        /// <remarks>
        /// This method creates instances of <see cref="authorizationProviders"/> by order and
        /// calls <see cref="AuthorizationProvider.SetPermissions"/> to build permission list.
        /// So, providers should not use dependency injection.
        /// </remarks>
        public static IReadOnlyList<Permission> GetAllPermissions(params AuthorizationProvider[] authorizationProviders)
        {
            return new InternalPermissionFinder(authorizationProviders).GetAllPermissions();
        }

        internal class InternalPermissionFinder : PermissionDefinitionContextBase
        {
            public InternalPermissionFinder(params AuthorizationProvider[] authorizationProviders)
            {
                foreach (var provider in authorizationProviders)
                {
                    provider.SetPermissions(this);
                }

                Permissions.AddAllPermissions();
            }

            public IReadOnlyList<Permission> GetAllPermissions()
            {
                return Permissions.Values.ToImmutableList();
            }
        }
    }
}