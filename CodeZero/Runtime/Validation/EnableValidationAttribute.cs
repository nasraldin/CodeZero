//  <copyright file="EnableValidationAttribute.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Runtime.Validation
{
    /// <summary>
    /// Can be added to a method to enable auto validation if validation is disabled for it's class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EnableValidationAttribute : Attribute
    {

    }
}