//  <copyright file="FeatureManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using CodeZero.Dependency;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Implements <see cref="IFeatureManager"/>.
    /// </summary>
    internal class FeatureManager : FeatureDefinitionContextBase, IFeatureManager, ISingletonDependency
    {
        private readonly IIocManager _iocManager;
        private readonly IFeatureConfiguration _featureConfiguration;

        /// <summary>
        /// Creates a new <see cref="FeatureManager"/> object
        /// </summary>
        /// <param name="iocManager">IOC Manager</param>
        /// <param name="featureConfiguration">Feature configuration</param>
        public FeatureManager(IIocManager iocManager, IFeatureConfiguration featureConfiguration)
        {
            _iocManager = iocManager;
            _featureConfiguration = featureConfiguration;
        }

        /// <summary>
        /// Initializes this <see cref="FeatureManager"/>
        /// </summary>
        public void Initialize()
        {
            foreach (var providerType in _featureConfiguration.Providers)
            {
                using (var provider = CreateProvider(providerType))
                {
                    provider.Object.SetFeatures(this);
                }
            }

            Features.AddAllFeatures();
        }

        /// <summary>
        /// Gets a feature by its given name
        /// </summary>
        /// <param name="name">Name of the feature</param>
        public Feature Get(string name)
        {
            var feature = GetOrNull(name);
            if (feature == null)
            {
                throw new CodeZeroException("There is no feature with name: " + name);
            }

            return feature;
        }

        /// <summary>
        /// Gets all the features
        /// </summary>
        public IReadOnlyList<Feature> GetAll()
        {
            return Features.Values.ToImmutableList();
        }

        private IDisposableDependencyObjectWrapper<FeatureProvider> CreateProvider(Type providerType)
        {
            return _iocManager.ResolveAsDisposable<FeatureProvider>(providerType);
        }
    }
}
