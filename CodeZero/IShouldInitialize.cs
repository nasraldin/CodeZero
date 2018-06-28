//  <copyright file="IShouldInitialize.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Castle.Core;

namespace CodeZero
{
    /// <summary>
    /// Defines interface for objects those should be Initialized before using it.
    /// If the object resolved using dependency injection, <see cref="IInitializable.Initialize"/>
    /// method is automatically called just after creation of the object.
    /// </summary>
    public interface IShouldInitialize : IInitializable
    {
        
    }
}