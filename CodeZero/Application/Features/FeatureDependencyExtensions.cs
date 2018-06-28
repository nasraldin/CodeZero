//  <copyright file="FeatureDependencyExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Threading;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Extension methods for <see cref="IFeatureDependency"/>.
    /// </summary>
    public static class FeatureDependencyExtensions
    {
        /// <summary>
        /// Checks dependent features and returns true if dependencies are satisfied.
        /// </summary>
        /// <param name="featureDependency">The feature dependency.</param>
        /// <param name="context">The context.</param>
        public static bool IsSatisfied(this IFeatureDependency featureDependency, IFeatureDependencyContext context)
        {
            return AsyncHelper.RunSync(() => featureDependency.IsSatisfiedAsync(context));
        }
    }
}