//  <copyright file="FeatureScopes.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Scopes of a <see cref="Feature"/>.
    /// </summary>
    [Flags]
    public enum FeatureScopes
    {
        /// <summary>
        /// This <see cref="Feature"/> can be enabled/disabled per edition.
        /// </summary>
        Edition = 1,

        /// <summary>
        /// This Feature<see cref="Feature"/> can be enabled/disabled per tenant.
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// This <see cref="Feature"/> can be enabled/disabled per tenant and edition.
        /// </summary>
        All = 3
    }
}