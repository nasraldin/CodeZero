//  <copyright file="HasMenuItemDefinitionsExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Collections.Extensions;

namespace CodeZero.Application.Navigation
{
    /// <summary>
    /// Defines extension methods for <see cref="IHasMenuItemDefinitions"/>.
    /// </summary>
    public static class HasMenuItemDefinitionsExtensions
    {
        /// <summary>
        /// Searches and gets a <see cref="MenuItemDefinition"/> by it's unique name.
        /// Throws exception if can not find.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="name">Unique name of the source</param>
        public static MenuItemDefinition GetItemByName(this IHasMenuItemDefinitions source, string name)
        {
            var item = GetItemByNameOrNull(source, name);
            if (item == null)
            {
                throw new ArgumentException("There is no source item with given name: " + name, "name");
            }

            return item;
        }

        /// <summary>
        /// Searches all menu items (recursively) in the source and gets a <see cref="MenuItemDefinition"/> by it's unique name.
        /// Returns null if can not find.
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="name">Unique name of the source</param>
        public static MenuItemDefinition GetItemByNameOrNull(this IHasMenuItemDefinitions source, string name)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Items.IsNullOrEmpty())
            {
                return null;
            }

            foreach (var subItem in source.Items)
            {
                if (subItem.Name == name)
                {
                    return subItem;
                }

                var subItemSearchResult = GetItemByNameOrNull(subItem, name);
                if (subItemSearchResult != null)
                {
                    return subItemSearchResult;
                }
            }

            return null;
        }
    }
}