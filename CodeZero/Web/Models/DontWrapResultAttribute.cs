//  <copyright file="DontWrapResultAttribute.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Web.Models
{
    /// <summary>
    /// A shortcut for <see cref="WrapResultAttribute"/> to disable wrapping by default.
    /// It sets false to <see cref="WrapResultAttribute.WrapOnSuccess"/> and <see cref="WrapResultAttribute.WrapOnError"/>  properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class DontWrapResultAttribute : WrapResultAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DontWrapResultAttribute"/> class.
        /// </summary>
        public DontWrapResultAttribute()
            : base(false, false)
        {

        }
    }
}