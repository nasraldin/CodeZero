//  <copyright file="ISettingValue.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Configuration
{
    /// <summary>
    /// Represents value of a setting.
    /// </summary>
    public interface ISettingValue
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Value of the setting.
        /// </summary>
        string Value { get; }
    }
}