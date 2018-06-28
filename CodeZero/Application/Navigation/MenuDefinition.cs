//  <copyright file="MenuDefinition.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using CodeZero.Localization;

namespace CodeZero.Application.Navigation
{
    /// <summary>
    /// Represents a navigation menu for an application.
    /// </summary>
    public class MenuDefinition : IHasMenuItemDefinitions
    {
        /// <summary>
        /// Unique name of the menu in the application. Required.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the menu. Required.
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// Can be used to store a custom object related to this menu. Optional.
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Menu items (first level).
        /// </summary>
        public IList<MenuItemDefinition> Items { get; set; }

        /// <summary>
        /// Creates a new <see cref="MenuDefinition"/> object.
        /// </summary>
        /// <param name="name">Unique name of the menu</param>
        /// <param name="displayName">Display name of the menu</param>
        /// <param name="customData">Can be used to store a custom object related to this menu.</param>
        public MenuDefinition(string name, ILocalizableString displayName, object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Menu name can not be empty or null.");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("displayName", "Display name of the menu can not be null.");
            }

            Name = name;
            DisplayName = displayName;
            CustomData = customData;

            Items = new List<MenuItemDefinition>();
        }

        /// <summary>
        /// Adds a <see cref="MenuItemDefinition"/> to <see cref="Items"/>.
        /// </summary>
        /// <param name="menuItem"><see cref="MenuItemDefinition"/> to be added</param>
        /// <returns>This <see cref="MenuDefinition"/> object</returns>
        public MenuDefinition AddItem(MenuItemDefinition menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
    }
}
