//  <copyright file="MultiTenancySides.cs" project="CodeZero" solution="CodeZero">
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
    /// Represents sides in a multi tenancy application.
    /// </summary>
    [Flags]
    public enum MultiTenancySides
    {
        /// <summary>
        /// Tenant side.
        /// </summary>
        Tenant = 1,
        
        /// <summary>
        /// Host (tenancy owner) side.
        /// </summary>
        Host = 2
    }
}