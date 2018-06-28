//  <copyright file="UserMenu.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Localization;

namespace CodeZero.Application.Navigation
{
    /// <summary>
    /// Represents a menu shown to the user.
    /// </summary>
    public class UserMenu
    {
        /// <summary>
        /// Unique name of the menu in the application. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the menu.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// A custom object related to this menu item.
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Menu items (first level).
        /// </summary>
        public IList<UserMenuItem> Items { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserMenu"/> object.
        /// </summary>
        public UserMenu()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="UserMenu"/> object from given <see cref="MenuDefinition"/>.
        /// </summary>
        internal UserMenu(MenuDefinition menuDefinition, ILocalizationContext localizationContext)
        {
            Name = menuDefinition.Name;
            DisplayName = menuDefinition.DisplayName.Localize(localizationContext);
            CustomData = menuDefinition.CustomData;
            Items = new List<UserMenuItem>();
        }
    }
}