//  <copyright file="DateTimeRangeExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using CodeZero.Timing;

namespace CodeZero.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDateTimeRange"/>.
    /// </summary>
    public static class DateTimeRangeExtensions
    {
        /// <summary>
        /// Sets date range to given target.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void SetTo(this IDateTimeRange source, IDateTimeRange target)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// Sets date range from given source.
        /// </summary>
        public static void SetFrom(this IDateTimeRange target, IDateTimeRange source)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// Returns all the days of a datetime range.
        /// </summary>
        /// <param name="dateRange">The date range.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRange(this IDateTimeRange dateRange)
        {
            return Enumerable.Range(0, (dateRange.TimeSpan).Days)
                .Select(offset => new DateTime(
                    dateRange.StartTime.AddDays(offset).Year,
                    dateRange.StartTime.AddDays(offset).Month,
                    dateRange.StartTime.AddDays(offset).Day));
        }

        /// <summary>
        /// Returns all the days in a range.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRange(DateTime start, DateTime end)
        {
            return new DateTimeRange(start, end).DaysInRange();
        }
    }
}