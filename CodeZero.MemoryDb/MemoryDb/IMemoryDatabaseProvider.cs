//  <copyright file="IMemoryDatabaseProvider.cs" project="CodeZero.MemoryDb" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.MemoryDb
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MemoryDatabase"/> object.
    /// </summary>
    public interface IMemoryDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MemoryDatabase"/>.
        /// </summary>
        MemoryDatabase Database { get; }
    }
}