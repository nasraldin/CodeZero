//  <copyright file="LocalizationContext.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;

namespace CodeZero.Localization
{
    /// <summary>
    /// Implements <see cref="ILocalizationContext"/>.
    /// </summary>
    public class LocalizationContext : ILocalizationContext, ISingletonDependency
    {
        public ILocalizationManager LocalizationManager { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationContext"/> class.
        /// </summary>
        /// <param name="localizationManager">The localization manager.</param>
        public LocalizationContext(ILocalizationManager localizationManager)
        {
            LocalizationManager = localizationManager;
        }
    }
}