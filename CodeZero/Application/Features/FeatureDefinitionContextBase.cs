//  <copyright file="FeatureDefinitionContextBase.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Collections.Extensions;
using CodeZero.Localization;
using CodeZero.UI.Inputs;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Base for implementing <see cref="IFeatureDefinitionContext"/>.
    /// </summary>
    public abstract class FeatureDefinitionContextBase : IFeatureDefinitionContext
    {
        protected readonly FeatureDictionary Features;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDefinitionContextBase"/> class.
        /// </summary>
        protected FeatureDefinitionContextBase()
        {
            Features = new FeatureDictionary();
        }

        /// <summary>
        /// Creates a new feature.
        /// </summary>
        /// <param name="name">Unique name of the feature</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="displayName">Display name of the feature</param>
        /// <param name="description">A brief description for this feature</param>
        /// <param name="scope">Feature scope</param>
        /// <param name="inputType">Input type</param>
        public Feature Create(string name, string defaultValue, ILocalizableString displayName = null,
            ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null)
        {
            if (Features.ContainsKey(name))
            {
                throw new CodeZeroException("There is already a feature with name: " + name);
            }

            var feature = new Feature(name, defaultValue, displayName, description, scope, inputType);
            Features[feature.Name] = feature;
            return feature;

        }

        /// <summary>
        /// Gets a feature with a given name, or null if can not be found.
        /// </summary>
        /// <param name="name">Unique name of the feature</param>
        /// <returns>
        ///   <see cref="Feature" /> object or null
        /// </returns>
        public Feature GetOrNull(string name)
        {
            return Features.GetOrDefault(name);
        }
    }
}