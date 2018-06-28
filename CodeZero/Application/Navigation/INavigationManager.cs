//  <copyright file="INavigationManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Application.Navigation
{
    /// <summary>
    /// Manages navigation in the application.
    /// </summary>
    public interface INavigationManager
    {
        /// <summary>
        /// All menus defined in the application.
        /// </summary>
        IDictionary<string, MenuDefinition> Menus { get; }

        /// <summary>
        /// Gets the main menu of the application.
        /// A shortcut of <see cref="Menus"/>["MainMenu"].
        /// </summary>
        MenuDefinition MainMenu { get; }
    }
}
