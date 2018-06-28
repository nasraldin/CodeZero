//  <copyright file="CodeZeroMemoryDbConfigurationExtensions.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.MemoryDb.Configuration;

namespace CodeZero.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure CodeZero MemoryDb module.
    /// </summary>
    public static class CodeZeroMemoryDbConfigurationExtensions
    {
        /// <summary>
        /// Used to configure CodeZero MemoryDb module.
        /// </summary>
        public static ICodeZeroMemoryDbModuleConfiguration CodeZeroMemoryDb(this IModuleConfigurations configurations)
        {
            return configurations.CodeZeroConfiguration.Get<ICodeZeroMemoryDbModuleConfiguration>();
        }
    }
}