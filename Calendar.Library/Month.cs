using System.Collections.Generic;
using System.Globalization;

namespace Calendar.Library
{
    class Month
    {
        private const int FEBRUARY = 1;
        private static readonly int[] DaysPerMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static readonly int WIDTH = Week.HEADER.Length;

        public static readonly string[] Names = DateTimeFormatInfo.InvariantInfo.MonthNames.GetRange(0, 12);

        private static int DaysInMonth(int month, int year)
        {
            var days = DaysPerMonth[month];

            if (month == FEBRUARY && Year.IsLeap(year))
            {
                days++;
            }

            return days;
        }

        public static List<string> Generate(int year, int month)
        {
            var daysInMonth = DaysInMonth(month, year);

            var weeks = new List<string>();

            var day = 1;

            while (day > 0)
            {
                var week = Week.Generate(year, month, day, daysInMonth, out day);

                weeks.Add(string.Join(" ", week));
            }

            return weeks;
        }
    }
}
