//  <copyright file="IFeatureDefinitionContext.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Localization;
using CodeZero.UI.Inputs;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Used in the <see cref="FeatureProvider.SetFeatures"/> method as context.
    /// </summary>
    public interface IFeatureDefinitionContext
    {
        /// <summary>
        /// Creates a new feature.
        /// </summary>
        /// <param name="name">Unique name of the feature</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="displayName">Display name of the feature</param>
        /// <param name="description">A brief description for this feature</param>
        /// <param name="scope">Feature scope</param>
        /// <param name="inputType">Input type</param>
        Feature Create(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null);

        /// <summary>
        /// Gets a feature with a given name or null if it can not be found.
        /// </summary>
        /// <param name="name">Unique name of the feature</param>
        /// <returns><see cref="Feature"/> object or null</returns>
        Feature GetOrNull(string name);
    }
}