//  <copyright file="ComboboxInputType.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Runtime.Validation;

namespace CodeZero.UI.Inputs
{
    /// <summary>
    /// Combobox value UI type.
    /// </summary>
    [Serializable]
    [InputType("COMBOBOX")]
    public class ComboboxInputType : InputTypeBase
    {
        public ILocalizableComboboxItemSource ItemSource { get; set; }

        public ComboboxInputType()
        {

        }

        public ComboboxInputType(ILocalizableComboboxItemSource itemSource)
        {
            ItemSource = itemSource;
        }

        public ComboboxInputType(ILocalizableComboboxItemSource itemSource, IValueValidator validator)
            : base(validator)
        {
            ItemSource = itemSource;
        }
    }
}