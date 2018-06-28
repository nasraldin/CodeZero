//  <copyright file="FeatureConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Collections;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Internal implementation for <see cref="IFeatureConfiguration"/>.
    /// </summary>
    internal class FeatureConfiguration : IFeatureConfiguration
    {
        /// <summary>
        /// Reference to the feature providers.
        /// </summary>
        public ITypeList<FeatureProvider> Providers { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureConfiguration"/> class.
        /// </summary>
        public FeatureConfiguration()
        {
            Providers = new TypeList<FeatureProvider>();
        }
    }
}