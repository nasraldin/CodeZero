//  <copyright file="IEntityChangeSetReasonProvider.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.EntityHistory
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface IEntityChangeSetReasonProvider
    {
        /// <summary>
        /// Gets current Reason or null.
        /// </summary>
        string Reason { get; }

        /// <summary>
        /// Used to change <see cref="Reason"/> for a limited scope.
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        IDisposable Use(string reason);
    }
}
