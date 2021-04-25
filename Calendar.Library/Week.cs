using System;

namespace Calendar.Library
{
    class Week
    {
        private const int DAY_WIDTH = 2;
        private const int LENGTH = 7;
        public const string HEADER = " S  M  T  W  T  F  S";

        public static string Generate(int year, int month, int day, int daysInMonth, out int nextDay)
        {
            var firstDay = (int)Utilities.DayOfWeek(new DateTime(year, month + 1, day));

            var week = new string[LENGTH].Fill(' '.Repeat(DAY_WIDTH));

            for (var i = firstDay; i < LENGTH && day <= daysInMonth; i++)
            {
                week[i] = day++.ToString().PadLeft(DAY_WIDTH);
            }

            nextDay = day <= daysInMonth ? day : -1;

            return string.Join(" ", week);
        }
    }
}
