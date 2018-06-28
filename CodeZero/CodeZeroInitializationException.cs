//  <copyright file="CodeZeroInitializationException.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Runtime.Serialization;

namespace CodeZero
{
    /// <summary>
    /// This exception is thrown if a problem on CodeZero initialization progress.
    /// </summary>
    [Serializable]
    public class CodeZeroInitializationException : CodeZeroException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CodeZeroInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public CodeZeroInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CodeZeroInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CodeZeroInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
