//  <copyright file="StaticLocalizableComboboxItemSource.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.UI.Inputs
{
    public class StaticLocalizableComboboxItemSource : ILocalizableComboboxItemSource
    {
        public ICollection<ILocalizableComboboxItem> Items { get; private set; }

        public StaticLocalizableComboboxItemSource(params ILocalizableComboboxItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Length <= 0)
            {
                throw new ArgumentException("Items can not be empty!");
            }

            Items = items;
        }
    }
}