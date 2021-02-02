using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeZero.Shared.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// ClearTime Time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ClearTime(this DateTime dateTime)
        {
            return dateTime.Subtract(
                new TimeSpan(
                    0,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond
                )
            );
        }

        /// <summary>
        /// Converts a DateTime to a Unix Timestamp
        /// </summary>
        /// <param name="target">This DateTime</param>
        /// <returns></returns>
        public static double ToUnixTimestamp(this DateTime target)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = target - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Converts a Unix Timestamp in to a DateTime
        /// </summary>
        /// <param name="unixTime">This Unix Timestamp</param>
        /// <returns></returns>
        public static DateTime FromUnixTimestamp(this double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Gets the value of the End of the day (23:59)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToDayEnd(this DateTime target)
        {
            return target.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Gets the First Date of the week for the specified date
        /// </summary>
        /// <param name="dt">this DateTime</param>
        /// <param name="startOfWeek">The Start Day of the Week (ie, Sunday/Monday)</param>
        /// <returns>The First Date of the week</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;

            if (diff < 0)
                diff += 7;

            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Returns all the days of a month.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysOfMonth(int year, int month)
        {
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day + 1));
        }

        /// <summary>
        /// Determines the Nth instance of a Date's DayOfWeek in a month
        /// </summary>
        /// <returns></returns>
        /// <example>11/29/2011 would return 5, because it is the 5th Tuesday of each month</example>
        public static int WeekDayInstanceOfMonth(this DateTime dateTime)
        {
            var y = 0;
            return DaysOfMonth(dateTime.Year, dateTime.Month)
                .Where(date => dateTime.DayOfWeek.Equals(date.DayOfWeek))
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.date.Equals(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day)))
                .Select(x => x.n).FirstOrDefault();
        }

        /// <summary>
        /// Gets the total days in a month
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static int TotalDaysInMonth(this DateTime dateTime)
        {
            return DaysOfMonth(dateTime.Year, dateTime.Month).Count();
        }

        /// <summary>
        /// Takes any date and returns it's value as an Unspecified DateTime.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeUnspecified(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return date;
            }

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Trims the milliseconds off of a datetime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime TrimMilliseconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        }

        /// <summary>
        /// Compares two datettime objects ignoring minutes and seconds.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCompare"></param>
        /// <returns></returns>
        public static int CompareWithoutMinutes(this DateTime source, DateTime toCompare)
        {
            source = new DateTime(source.Year, source.Month, source.Day, source.Hour, 0, 0);
            toCompare = new DateTime(toCompare.Year, toCompare.Month, toCompare.Day, toCompare.Hour, 0, 0);

            return source.CompareTo(toCompare);
        }

        /// <summary>
        /// Returns whether the given date is the last day of the month.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsLastDayOfTheMonth(this DateTime dateTime)
        {
            return dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Representing dates in a user friendly way.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToReadableTimespan(this DateTime dateTime)
        {
            //string FormattedDate;
            //if (dateTime.Date == DateTime.Today)
            //{
            //    FormattedDate = "Today";
            //}
            //else if (dateTime.Date == DateTime.Today.AddDays(-1))
            //{
            //    FormattedDate = "Yesterday";
            //}
            //else if (dateTime.Date > DateTime.Today.AddDays(-6))
            //{
            //    // *** Show the Day of the week
            //    FormattedDate = dateTime.ToString("dddd").ToString();
            //}
            //else
            //{
            //    FormattedDate = dateTime.ToString("MMMM dd, yyyy");
            //}

            ////append the time portion to the output
            //FormattedDate += " @ " + dateTime.ToString("t").ToLower();
            //return FormattedDate;

            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = DateTime.Now.Subtract(dateTime);

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                    dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} weeks ago",
                    Math.Ceiling((double)dayDiff / 7));
            }
            return null;
        }

        /// <summary>
        /// Time passed since specified value in user friendly string e.g '3 days ago'
        /// </summary>
        /// <param name="dateTime">Value to convert in user friendly string</param>
        /// <param name="currentTime">Value to take reference as current time when converting to user friendly string</param>
        /// <returns>User friendly datetime string e.g '3 days ago'</returns>
        public static string When(this DateTime dateTime, DateTime currentTime)
        {
            var timespan = currentTime - dateTime;

            if (timespan.Days > 365)
                return string.Format("{0} year{1} ago", timespan.Days / 365, (timespan.Days / 365) > 1 ? "s" : "");

            if (timespan.Days > 30)
                return string.Format("{0} month{1} ago", timespan.Days / 30, (timespan.Days / 30) > 1 ? "s" : "");

            if (timespan.Days > 0)
                return string.Format("{0} day{1} ago", timespan.Days, timespan.Days > 1 ? "s" : "");

            if (timespan.Hours > 0)
                return string.Format("{0} hour{1} ago", timespan.Hours, timespan.Hours > 1 ? "s" : "");

            if (timespan.Minutes > 0)
                return string.Format("{0} minute{1} ago", timespan.Minutes, timespan.Minutes > 1 ? "s" : "");

            return "A moment ago";
        }

        /// <summary>
        /// Get the actual age of a person.
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month || DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Today.Year - dateOfBirth.Year;
        }
    }
}