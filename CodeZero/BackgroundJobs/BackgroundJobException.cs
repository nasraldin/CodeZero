//  <copyright file="BackgroundJobException.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace CodeZero.BackgroundJobs
{
    [Serializable]
    public class BackgroundJobException : CodeZeroException
    {
        [CanBeNull]
        public BackgroundJobInfo BackgroundJob { get; set; }

        [CanBeNull]
        public object JobObject { get; set; }

        /// <summary>
        /// Creates a new <see cref="BackgroundJobException"/> object.
        /// </summary>
        public BackgroundJobException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="BackgroundJobException"/> object.
        /// </summary>
        public BackgroundJobException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="BackgroundJobException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BackgroundJobException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
