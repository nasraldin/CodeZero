//  <copyright file="CodeZeroEntityFrameworkGraphDiffConfigurationExtensions.cs" project="CodeZero.EntityFramework.GraphDiff" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Linq;
using CodeZero.Configuration.Startup;
using CodeZero.EntityFramework.GraphDiff.Mapping;

namespace CodeZero.EntityFramework.GraphDiff.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure CodeZero.EntityFramework.GraphDiff module.
    /// </summary>
    public static class CodeZeroEntityFrameworkGraphDiffConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero.EntityFramework.GraphDiff module.
        /// </summary>
        public static ICodeZeroEntityFrameworkGraphDiffModuleConfiguration CodeZeroEfGraphDiff(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroEntityFrameworkGraphDiffModuleConfiguration>();
        }

        /// <summary>
        /// Used to provide a mappings for the CodeZero.EntityFramework.GraphDiff module.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="entityMappings"></param>
        public static void UseMappings(this ICodeZeroEntityFrameworkGraphDiffModuleConfiguration configuration, IEnumerable<EntityMapping> entityMappings)
        {
            configuration.EntityMappings = entityMappings.ToList();
        }
    }
}
