//  <copyright file="NamedTypeSelectorListExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeZero
{
    public static class NamedTypeSelectorListExtensions
    {
        /// <summary>
        /// Add list of types to the list.
        /// </summary>
        /// <param name="list">List of NamedTypeSelector items</param>
        /// <param name="name">An arbitrary but unique name (can be later used to remove types from the list)</param>
        /// <param name="types"></param>
        public static void Add(this IList<NamedTypeSelector> list, string name, params Type[] types)
        {
            Check.NotNull(list, nameof(list));
            Check.NotNull(name, nameof(name));
            Check.NotNull(types, nameof(types));

            list.Add(new NamedTypeSelector(name, type => types.Any(type.IsAssignableFrom)));
        }
    }
}