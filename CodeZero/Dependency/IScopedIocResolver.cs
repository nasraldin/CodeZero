//  <copyright file="IScopedIocResolver.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Dependency
{
    /// <summary>
    ///     This interface is used to wrap a scope for batch resolvings in a single <c>using</c> statement.
    ///     It inherits <see cref="IDisposable" /> and <see cref="IIocResolver" />, so resolved objects can be easily and batch
    ///     manner released by IocResolver.
    ///     In <see cref="IDisposable.Dispose" /> method, <see cref="IIocResolver.Release" /> is called to dispose the object.
    /// </summary>
    public interface IScopedIocResolver : IIocResolver, IDisposable { }
}