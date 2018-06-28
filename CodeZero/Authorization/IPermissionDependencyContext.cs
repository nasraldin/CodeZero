//  <copyright file="IPermissionDependencyContext.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Application.Features;
using CodeZero.Dependency;

namespace CodeZero.Authorization
{
    /// <summary>
    /// Permission dependency context.
    /// </summary>
    public interface IPermissionDependencyContext
    {
        /// <summary>
        /// The user which requires permission. Can be null if no user.
        /// </summary>
        UserIdentifier User { get; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IPermissionChecker PermissionChecker { get; }
    }
}