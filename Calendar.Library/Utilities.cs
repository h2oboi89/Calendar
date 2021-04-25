using System;
using System.Collections.Generic;

namespace Calendar.Library
{
    /// <summary>
    /// Utility Function
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Determines which day of the week (Sunday, Monday, ...) a specified day falls on.
        /// Duplication of built in function, but we wanted to implement it ourselves.
        /// </summary>
        /// <param name="date">Date to get day of week for.</param>
        /// <returns>Day of the week a date falls on.</returns>
        public static DayOfWeek DayOfWeek(DateTime date)
        {
            var day = date.Day;
            var month = date.Month < 3 ? date.Month + 12 : date.Month;

            var year = date.Year;

            if (month > 12)
            {
                year -= 1;
            }

            var century = year / 100;

            year %= 100;

            var a = day;
            var b = (13.0 * (month + 1) / 5).Floor();
            var c = year;
            var d = (year / 4.0).Floor();
            var e = (century / 4.0).Floor();
            var f = -2 * century;

            var dayOfWeek = (a + b + c + d + e + f).Modulo(7);

            dayOfWeek = (dayOfWeek + 6) % 7;

            return (DayOfWeek)dayOfWeek;
        }
    }
}
