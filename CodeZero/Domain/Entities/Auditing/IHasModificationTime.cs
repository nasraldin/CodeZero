//  <copyright file="IHasModificationTime.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Domain.Entities.Auditing
{
    /// <summary>
    /// An entity can implement this interface if <see cref="LastModificationTime"/> of this entity must be stored.
    /// <see cref="LastModificationTime"/> is automatically set when updating <see cref="Entity"/>.
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}