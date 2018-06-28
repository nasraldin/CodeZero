//  <copyright file="ValidationErrorInfo.cs" project="CodeZero.Web.Common" solution="CodeZero">
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
    /// Used to store information about a validation error.
    /// </summary>
    [Serializable]
    public class ValidationErrorInfo
    {
        /// <summary>
        /// Validation error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Relate invalid members (fields/properties).
        /// </summary>
        public string[] Members { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        public ValidationErrorInfo()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        /// <param name="message">Validation error message</param>
        public ValidationErrorInfo(string message)
        {
            Message = message;
        }
        
        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        /// <param name="message">Validation error message</param>
        /// <param name="members">Related invalid members</param>
        public ValidationErrorInfo(string message, string[] members)
            : this(message)
        {
            Members = members;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        /// <param name="message">Validation error message</param>
        /// <param name="member">Related invalid member</param>
        public ValidationErrorInfo(string message, string member)
            : this(message, new[] { member })
        {

        }
    }
}