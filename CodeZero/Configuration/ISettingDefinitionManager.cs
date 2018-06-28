//  <copyright file="ISettingDefinitionManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Configuration
{
    /// <summary>
    /// Defines setting definition manager.
    /// </summary>
    public interface ISettingDefinitionManager
    {
        /// <summary>
        /// Gets the <see cref="SettingDefinition"/> object with given unique name.
        /// Throws exception if can not find the setting.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <returns>The <see cref="SettingDefinition"/> object.</returns>
        SettingDefinition GetSettingDefinition(string name);

        /// <summary>
        /// Gets a list of all setting definitions.
        /// </summary>
        /// <returns>All settings.</returns>
        IReadOnlyList<SettingDefinition> GetAllSettingDefinitions();
    }
}
