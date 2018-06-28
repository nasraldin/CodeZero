//  <copyright file="HttpVerb.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Web
{
    /// <summary>
    /// Represents an HTTP verb.
    /// </summary>
    [Flags]
    public enum HttpVerb
    {
        /// <summary>
        /// GET
        /// </summary>
        Get,

        /// <summary>
        /// POST
        /// </summary>
        Post,

        /// <summary>
        /// PUT
        /// </summary>
        Put,

        /// <summary>
        /// DELETE
        /// </summary>
        Delete,

        /// <summary>
        /// OPTIONS
        /// </summary>
        Options,

        /// <summary>
        /// TRACE
        /// </summary>
        Trace,

        /// <summary>
        /// HEAD
        /// </summary>
        Head,

        /// <summary>
        /// PATCH
        /// </summary>
        Patch,
    }
}