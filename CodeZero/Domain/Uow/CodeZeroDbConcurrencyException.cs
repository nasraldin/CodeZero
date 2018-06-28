//  <copyright file="CodeZeroDbConcurrencyException.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Runtime.Serialization;

namespace CodeZero.Domain.Uow
{
    [Serializable]
    public class CodeZeroDbConcurrencyException : CodeZeroException
    {
        /// <summary>
        /// Creates a new <see cref="CodeZeroDbConcurrencyException"/> object.
        /// </summary>
        public CodeZeroDbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroException"/> object.
        /// </summary>
        public CodeZeroDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CodeZeroDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CodeZeroDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}