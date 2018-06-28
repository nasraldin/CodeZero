//  <copyright file="IZonedDateTimeRange.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeZero.Timing
{
    /// <summary>
    /// Defines interface for a DateTime range with timezone.
    /// </summary>
    public interface IZonedDateTimeRange : IDateTimeRange
    {
        /// <summary>
        /// The Timezone of the datetime range
        /// </summary>
        string Timezone { get; set; }

        /// <summary>
        /// The StartTime with Offset
        /// </summary>
        DateTimeOffset StartTimeOffset { get; set; }

        /// <summary>
        /// The EndTime with Offset
        /// </summary>
        DateTimeOffset EndTimeOffset { get; set; }

        /// <summary>
        /// The StartTime in UTC
        /// </summary>
        DateTime StartTimeUtc { get; set; }

        /// <summary>
        /// The EndTime in UTC
        /// </summary>
        DateTime EndTimeUtc { get; set; }
    }
}
