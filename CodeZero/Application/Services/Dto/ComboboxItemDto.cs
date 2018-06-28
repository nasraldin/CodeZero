//  <copyright file="ComboboxItemDto.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be used as a simple item for a combobox/list.
    /// </summary>
    [Serializable]
    public class ComboboxItemDto
    {
        /// <summary>
        /// Value of the item.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display text of the item.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Is selected?
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// </summary>
        public ComboboxItemDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// </summary>
        /// <param name="value">Value of the item</param>
        /// <param name="displayText">Display text of the item</param>
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}