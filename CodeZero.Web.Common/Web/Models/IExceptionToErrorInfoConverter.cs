//  <copyright file="IExceptionToErrorInfoConverter.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Web.Models
{
    /// <summary>
    /// This interface can be implemented to convert an <see cref="Exception"/> object to an <see cref="ErrorInfo"/> object. 
    /// Implements Chain Of Responsibility pattern.
    /// </summary>
    public interface IExceptionToErrorInfoConverter
    {
        /// <summary>
        /// Next converter. If this converter decide this exception is not known, it can call Next.Convert(...).
        /// </summary>
        IExceptionToErrorInfoConverter Next { set; }

        /// <summary>
        /// Converter method.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>Error info or null</returns>
        ErrorInfo Convert(Exception exception);
    }
}