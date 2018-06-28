//  <copyright file="MultiTenancySideAttribute.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.MultiTenancy
{
    /// <summary>
    /// Used to declare multi tenancy side of an object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Interface)]
    public class MultiTenancySideAttribute : Attribute
    {
        /// <summary>
        /// Multitenancy side.
        /// </summary>
        public MultiTenancySides Side { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenancySideAttribute"/> class.
        /// </summary>
        /// <param name="side">Multitenancy side.</param>
        public MultiTenancySideAttribute(MultiTenancySides side)
        {
            Side = side;
        }
    }
}