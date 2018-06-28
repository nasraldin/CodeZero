//  <copyright file="CodeZeroException.cs" project="CodeZero" solution="CodeZero">
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
    /// <inheritdoc />
    /// <summary>
    /// Base exception type for those are thrown by CodeZero system for CodeZero specific exceptions.
    /// </summary>
    [Serializable]
    public class CodeZeroException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.CodeZeroException" /> object.
        /// </summary>
        public CodeZeroException()
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.CodeZeroException" /> object.
        /// </summary>
        public CodeZeroException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.CodeZeroException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CodeZeroException(string message)
            : base(message)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.CodeZeroException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CodeZeroException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
