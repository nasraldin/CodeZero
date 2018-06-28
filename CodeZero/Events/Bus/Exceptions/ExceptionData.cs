//  <copyright file="ExceptionData.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events can be used to notify for an exception.
    /// </summary>
    public class ExceptionData : EventData
    {
        /// <summary>
        /// Exception object.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public ExceptionData(Exception exception)
        {
            Exception = exception;
        }
    }
}
