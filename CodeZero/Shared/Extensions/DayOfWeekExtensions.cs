using System;
using System.Globalization;
using System.Linq;

namespace CodeZero.Shared.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DayOfWeekExtensions"/>.
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// Check if a given <see cref="DayOfWeek"/> value is weekend.
        /// MENA = Middle East (Friday, Saturday)
        /// </summary>
        public static bool IsWeekend(this DayOfWeek dayOfWeek, bool mena = true)
        {
            //switch (dow)
            //{
            //    case DayOfWeek.Sunday:
            //    case DayOfWeek.Saturday:
            //        return false;

            //    default:
            //        return true;
            //}

            if (mena)
                return dayOfWeek.IsIn(DayOfWeek.Friday, DayOfWeek.Saturday);

            return dayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Check if a given <see cref="DayOfWeek"/> value is weekday.
        /// MENA = Middle East
        /// </summary>
        public static bool IsWeekday(this DayOfWeek dayOfWeek, bool mena = true)
        {
            if (mena)
                return dayOfWeek.IsIn(DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday);

            return dayOfWeek.IsIn(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }

        /// <summary>
        /// Finds the NTH week day of a month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="n">The nth instance.</param>
        /// <remarks>Compensates for 4th and 5th DayOfWeek of Month</remarks>
        public static DateTime FindNthWeekDayOfMonth(this DayOfWeek dayOfWeek, int year, int month, int n)
        {
            if (n < 1 || n > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            var y = 0;

            var daysOfMonth = DateTimeExtensions.DaysOfMonth(year, month);

            // compensate for "last DayOfWeek in month"
            var totalInstances = dayOfWeek.TotalInstancesInMonth(year, month);
            if (n == 5 && n > totalInstances)
                n = 4;

            var foundDate = daysOfMonth
                .Where(date => dayOfWeek.Equals(date.DayOfWeek))
                .OrderBy(date => date)
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.n.Equals(n)).Select(x => x.date).First(); //black magic wizardry

            return foundDate;
        }

        /// <summary>
        /// Finds the total number of instances of a specific DayOfWeek in a month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, int year, int month)
        {
            return DateTimeExtensions.DaysOfMonth(year, month).Count(date => dayOfWeek.Equals(date.DayOfWeek));
        }

        /// <summary>
        /// Gets the total number of instances of a specific DayOfWeek in a month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="dateTime">The date in a month.</param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, DateTime dateTime)
        {
            return dayOfWeek.TotalInstancesInMonth(dateTime.Year, dateTime.Month);
        }

        /// <summary>
        /// Add working day to a date, where working day means from Monday to Friday.
        /// and working in Middle East means from Sunday to Thursday.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="days"></param>
        /// <param name="mena"></param>
        /// <returns></returns>
        public static DateTime AddWorkdays(this DateTime startDate, int days, bool mena)
        {
            // start from a weekday        
            while (startDate.DayOfWeek.CheckIsWeekend(mena))
                startDate = startDate.AddDays(1.0);

            for (int i = 0; i < days; ++i)
            {
                startDate = startDate.AddDays(1.0);

                while (startDate.DayOfWeek.CheckIsWeekend(mena))
                    startDate = startDate.AddDays(1.0);
            }

            return startDate;
        }

        private static bool CheckIsWeekend(this DayOfWeek dow, bool mena)
        {
            return !dow.IsWeekday(mena);
        }

        ///// <summary>
        ///// Add working day to a date, where working day means from Monday to Friday.
        ///// </summary>
        ///// <param name="d"></param>
        ///// <param name="days"></param>
        ///// <returns></returns>
        //public static DateTime AddWorkDays(this DateTime d, int days)
        //{
        //    for (int i = 0; i < days; ++i)
        //    {
        //        if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
        //        {
        //            d = d.AddDays(1.0);
        //            days++;
        //            continue;
        //        }

        //        d = d.AddDays(1.0);
        //    }
        //    return d;
        //}

        ///// <summary>
        ///// Add working Middle East day to a date, where working day means from Sunday to Thursday.
        ///// </summary>
        ///// <param name="d"></param>
        ///// <param name="days"></param>
        ///// <returns></returns>
        //public static DateTime AddWorkDaysMENA(this DateTime d, int days)
        //{
        //    for (int i = 0; i < days; ++i)
        //    {
        //        if (d.DayOfWeek == DayOfWeek.Friday || d.DayOfWeek == DayOfWeek.Saturday)
        //        {
        //            d = d.AddDays(1.0);
        //            days++;
        //            continue;
        //        }

        //        d = d.AddDays(1.0);
        //    }
        //    return d;
        //}


        #region Elapsed extension
        /// <summary>
        /// Elapseds the time.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }
        #endregion

        #region Week of year
        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="weekrule">The weekrule.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, CalendarWeekRule weekrule, DayOfWeek firstDayOfWeek)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(datetime, weekrule, firstDayOfWeek);
        }
        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, DayOfWeek firstDayOfWeek)
        {
            DateTimeFormatInfo dateinf = new DateTimeFormatInfo();
            CalendarWeekRule weekrule = dateinf.CalendarWeekRule;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }
        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="weekrule">The weekrule.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, CalendarWeekRule weekrule)
        {
            DateTimeFormatInfo dateinf = new DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }
        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime)
        {
            DateTimeFormatInfo dateinf = new DateTimeFormatInfo();
            CalendarWeekRule weekrule = dateinf.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }
        #endregion

        #region Get Datetime for Day of Week
        /// <summary>
        /// Gets the date time for day of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="day">The day.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static DateTime GetDateTimeForDayOfWeek(this DateTime datetime, DayOfWeek day, DayOfWeek firstDayOfWeek)
        {
            int current = DaysFromFirstDayOfWeek(datetime.DayOfWeek, firstDayOfWeek);
            int resultday = DaysFromFirstDayOfWeek(day, firstDayOfWeek);
            return datetime.AddDays(resultday - current);
        }
        public static DateTime GetDateTimeForDayOfWeek(this DateTime datetime, DayOfWeek day)
        {
            DateTimeFormatInfo dateinf = new DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return GetDateTimeForDayOfWeek(datetime, day, firstDayOfWeek);
        }
        /// <summary>
        /// Firsts the date time of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static DateTime FirstDateTimeOfWeek(this DateTime datetime)
        {
            DateTimeFormatInfo dateinf = new DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return FirstDateTimeOfWeek(datetime, firstDayOfWeek);
        }
        /// <summary>
        /// Firsts the date time of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static DateTime FirstDateTimeOfWeek(this DateTime datetime, DayOfWeek firstDayOfWeek)
        {
            return datetime.AddDays(-DaysFromFirstDayOfWeek(datetime.DayOfWeek, firstDayOfWeek));
        }

        /// <summary>
        /// Dayses from first day of week.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        private static int DaysFromFirstDayOfWeek(DayOfWeek current, DayOfWeek firstDayOfWeek)
        {
            //Sunday = 0,Monday = 1,...,Saturday = 6
            int daysbetween = current - firstDayOfWeek;
            if (daysbetween < 0) daysbetween = 7 + daysbetween;
            return daysbetween;
        }
        #endregion

        public static string GetValueOrDefaultToString(this DateTime? datetime, string defaultvalue)
        {
            if (datetime == null) return defaultvalue;
            return datetime.Value.ToString();
        }

        public static string GetValueOrDefaultToString(this DateTime? datetime, string format, string defaultvalue)
        {
            if (datetime == null) return defaultvalue;
            return datetime.Value.ToString(format);
        }
    }
}