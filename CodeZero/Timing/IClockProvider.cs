//  <copyright file="IClockProvider.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Timing
{
    /// <summary>
    /// Defines interface to perform some common date-time operations.
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// Gets Now.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets kind.
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// Is that provider supports multiple time zone.
        /// </summary>
        bool SupportsMultipleTimezone { get; }

        /// <summary>
        /// Normalizes given <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized.</param>
        /// <returns>Normalized DateTime</returns>
        DateTime Normalize(DateTime dateTime);
    }
}