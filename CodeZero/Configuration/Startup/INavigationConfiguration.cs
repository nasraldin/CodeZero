//  <copyright file="INavigationConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Application.Navigation;
using CodeZero.Collections;

namespace CodeZero.Configuration.Startup
{
    /// <summary>
    /// Used to configure navigation.
    /// </summary>
    public interface INavigationConfiguration
    {
        /// <summary>
        /// List of navigation providers.
        /// </summary>
        ITypeList<NavigationProvider> Providers { get; }
    }
}