//  <copyright file="FeatureProvider.cs" project="CodeZero" solution="CodeZero">
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
    /// This class should be inherited in order to provide <see cref="Feature"/>s.
    /// </summary>
    public abstract class FeatureProvider : ITransientDependency
    {
        /// <summary>
        /// Used to set <see cref="Feature"/>s.
        /// </summary>
        /// <param name="context">Feature definition context</param>
        public abstract void SetFeatures(IFeatureDefinitionContext context);
    }
}