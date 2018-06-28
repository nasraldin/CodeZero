//  <copyright file="IFeatureDependencyContext.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Used in the <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// </summary>
    public interface IFeatureDependencyContext
    {
        /// <summary>
        /// Tenant id which requires the feature.
        /// Null for current tenant.
        /// </summary>
        int? TenantId { get; }

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
        IFeatureChecker FeatureChecker { get; }
    }
}