//  <copyright file="CodeZeroAllowAnonymousAttribute.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Authorization
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="CodeZeroAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class CodeZeroAllowAnonymousAttribute : Attribute, ICodeZeroAllowAnonymousAttribute
    {

    }
}