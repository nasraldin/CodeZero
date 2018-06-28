//  <copyright file="IErrorInfoBuilder.cs" project="CodeZero.Web.Common" solution="CodeZero">
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
    /// This interface is used to build <see cref="ErrorInfo"/> objects.
    /// </summary>
    public interface IErrorInfoBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/> using the given <paramref name="exception"/> object.
        /// </summary>
        /// <param name="exception">The exception object</param>
        /// <returns>Created <see cref="ErrorInfo"/> object</returns>
        ErrorInfo BuildForException(Exception exception);

        /// <summary>
        /// Adds an <see cref="IExceptionToErrorInfoConverter"/> object.
        /// </summary>
        /// <param name="converter">Converter</param>
        void AddExceptionConverter(IExceptionToErrorInfoConverter converter);
    }
}