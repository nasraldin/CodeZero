//  <copyright file="PermissionDependencyExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Threading;

namespace CodeZero.Authorization
{
    /// <summary>
    /// Extension methods for <see cref="IPermissionDependency"/>.
    /// </summary>
    public static class PermissionDependencyExtensions
    {
        /// <summary>
        /// Checks if permission dependency is satisfied.
        /// </summary>
        /// <param name="permissionDependency">The permission dependency</param>
        /// <param name="context">Context.</param>
        public static bool IsSatisfied(this IPermissionDependency permissionDependency, IPermissionDependencyContext context)
        {
            return AsyncHelper.RunSync(() => permissionDependency.IsSatisfiedAsync(context));
        }
    }
}