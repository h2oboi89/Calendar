using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Determines if a year is a leap year.
        /// </summary>
        /// <param name="year">Year to check.</param>
        /// <returns>True if leap year; otherwise false.</returns>
        public static bool IsLeapYear(int year)
        {
            if (year % 400 == 0) return true;
            else if (year % 100 == 0) return false;
            else if (year % 4 == 0) return true;
            else return false;
        }

        private static readonly int[] DaysPerMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// Determines how many days are a month.
        /// </summary>
        /// <param name="month">Month to check.</param>
        /// <param name="year">Year month is in.</param>
        /// <returns>Number of days in the specified month.</returns>
        public static int DaysInMonth(int month, int year)
        {
            const int FEBRUARY = 1;

            var days = DaysPerMonth[month];

            if (month == FEBRUARY && IsLeapYear(year))
            {
                days++;
            }

            return days;
        }

        private const int DAY_WIDTH = 2;
        private const string WEEK_HEADER = " S  M  T  W  T  F  S";
        private static readonly int MONTH_WIDTH = WEEK_HEADER.Length;
        private const int INTER_MONTH_SPACE = 2;
        private static readonly string[] monthNames = new System.Globalization.CultureInfo("en-US").DateTimeFormat.MonthNames.Take(12).ToArray();


        private static List<string> GenerateWeeks(int year, int month)
        {
            var weeks = new List<string>();

            var firstDay = (int)DayOfWeek(new DateTime(year, month + 1, 1));

            var day = 0;
            var daysPerMonth = DaysInMonth(month, year);

            while (day < daysPerMonth)
            {
                var week = new string[7].Fill(' '.Repeat(DAY_WIDTH));

                var start = day == 0 ? firstDay : 0;

                for (var i = start; i < week.Length && day < daysPerMonth; i++)
                {
                    week[i] = (++day).ToString().PadLeft(DAY_WIDTH);
                }

                weeks.Add(string.Join(" ", week));
            }

            return weeks;
        }

        private static void GenerateRowOfMonths(int year, ref int month, string[] group, List<string> output, int totalWidth)
        {
            var monthNames = new List<string>();
            var weekHeaders = new List<string>();

            var weeks = new List<List<string>>();

            foreach (var currentMonth in group)
            {
                if (currentMonth != null)
                {
                    monthNames.Add(currentMonth.Center(MONTH_WIDTH));
                    weekHeaders.Add(WEEK_HEADER);

                    weeks.Add(GenerateWeeks(year, month++));
                }
            }

            weeks.Equalize(' '.Repeat(WEEK_HEADER.Length));

            output.Add(string.Join(' '.Repeat(INTER_MONTH_SPACE), monthNames));
            output.Add('-'.Repeat(totalWidth));
            output.Add(string.Join(' '.Repeat(INTER_MONTH_SPACE), weekHeaders));

            for (var i = 0; i < weeks[0].Count; i++)
            {
                var weekLine = new List<string>();

                for (var j = 0; j < weeks.Count; j++)
                {
                    weekLine.Add(weeks[j][i]);
                }

                output.Add(string.Join(' '.Repeat(INTER_MONTH_SPACE), weekLine));
            }

            output.Add(' '.Repeat(totalWidth));
        }

        /// <summary>
        /// Generates calendar for the specified year.
        /// </summary>
        /// <param name="year">Year to generate calendar for.</param>
        /// <param name="width">How wide (in months) the calendar should be before wrapping.</param>
        /// <returns>Collection of lines making up the calendar.</returns>
        public static IEnumerable<string> Generate(int year, int width)
        {
            var totalWidth = (width * MONTH_WIDTH) + ((width - 1) * INTER_MONTH_SPACE);
            var output = new List<string>
            {
                '='.Repeat(totalWidth),
                string.Empty,
                year.ToString().Center(totalWidth),
                string.Empty,
                '='.Repeat(totalWidth),
                string.Empty
            };

            var monthGroups = monthNames.Split(width);

            var month = 0;

            foreach (var group in monthGroups)
            {
                GenerateRowOfMonths(year, ref month, group, output, totalWidth);
            }

            return output;
        }
    }
}
