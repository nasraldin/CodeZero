//  <copyright file="CodeZeroRemoteCallException.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Runtime.Serialization;
using CodeZero.Web.Models;

namespace CodeZero.WebApi.Client
{
    /// <summary>
    /// This exception is thrown when a remote method call made and remote application sent an error message.
    /// </summary>
    [Serializable]
    public class CodeZeroRemoteCallException : CodeZeroException
    {
        /// <summary>
        /// Remote error information.
        /// </summary>
        public ErrorInfo ErrorInfo { get; set; }

        /// <summary>
        /// Creates a new <see cref="CodeZeroException"/> object.
        /// </summary>
        public CodeZeroRemoteCallException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroException"/> object.
        /// </summary>
        public CodeZeroRemoteCallException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CodeZeroException"/> object.
        /// </summary>
        /// <param name="errorInfo">Exception message</param>
        public CodeZeroRemoteCallException(ErrorInfo errorInfo)
            : base(errorInfo.Message)
        {
            ErrorInfo = errorInfo;
        }
    }
}
