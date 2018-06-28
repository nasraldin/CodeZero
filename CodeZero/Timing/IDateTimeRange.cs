//  <copyright file="IDateTimeRange.cs" project="CodeZero" solution="CodeZero">
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
    /// Defines interface for a DateTime range.
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// Start time of the datetime range.
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// End time of the datetime range.
        /// </summary>
        DateTime EndTime { get; set; }

        /// <summary>
        /// The time difference between the start and end times.
        /// </summary>
        TimeSpan TimeSpan { get; set; }
    }
}
