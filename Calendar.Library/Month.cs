using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Calendar.Library
{
    /// <summary>
    /// Represents a month.
    /// </summary>
    public class Month
    {
        private const int FEBRUARY = 1;
        private static readonly int[] DaysPerMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// Width in <see cref="char"/> when converted to a <see cref="string"/>.
        /// </summary>
        public static readonly int WIDTH = Week.HEADER.Length;

        /// <summary>
        /// English names of the months in a year.
        /// </summary>
        public static readonly string[] Names = DateTimeFormatInfo.InvariantInfo.MonthNames.GetRange(0, 12);

        /// <summary>
        /// Year associated with this <see cref="Month"/>
        /// </summary>
        public readonly int Year;

        /// <summary>
        /// Month value (1 - 12)
        /// </summary>
        public readonly int Value;

        private readonly List<Date> Days;

        /// <summary>
        /// Creates a new <see cref="Month"/> with the specified values.
        /// </summary>
        /// <param name="year">Year the month is in.</param>
        /// <param name="month">Month value. (1 - 12)</param>
        public Month(int year, int month)
        {
            Year = year;
            Value = month;

            var daysInMonth = DaysInMonth(month - 1, Year);

            Days = new List<Date>();

            var day = 1;

            while (Days.Count < daysInMonth)
            {
                var date = new Date(Year, Value, day++);

                Days.Add(date);
            }
        }

        /// <summary>
        /// Gets specified <see cref="Date"/> from this <see cref="Month"/>.
        /// </summary>
        /// <param name="day">The one-based index of the day to retrieve.</param>
        /// <returns>The specified <see cref="Date"/> from this <see cref="Month"/>.</returns>
        public Date this[int day] => Days[day - 1];

        /// <summary>
        /// Converts this month into a collection of <see cref="Week"/>s for printing.
        /// Will return at least <paramref name="weekCount"/> number of weeks.
        /// </summary>
        public IEnumerable<Week> AsWeeks(int weekCount = 0)
        {
            var days = new Queue<Date>(Days);

            var count = 0;
            while (days.Count > 0)
            {
                var week = Week.Generate(days);
                count++;

                yield return week;
            }

            while(count < weekCount)
            {
                yield return Week.Empty;
                count++;
            }
        }

        private static int DaysInMonth(int month, int year)
        {
            var days = DaysPerMonth[month];

            if (month == FEBRUARY && Library.Year.IsLeap(year))
            {
                days++;
            }

            return days;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Month"/>.
        /// Highlighting will be applied if any <see cref="Date"/> is in <paramref name="datesToHighlight"/>.
        /// </summary>
        /// <param name="datesToHighlight"><see cref="Date"/>s to add special highlighting to.</param>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Month"/>.</returns>
        public string ToString(List<Date> datesToHighlight)
        {
            var sb = new StringBuilder();

            sb.AppendLine(Names[Value - 1].Center(WIDTH));

            sb.AppendLine('-'.Repeat(WIDTH));

            sb.AppendLine(Week.HEADER);

            foreach (var week in AsWeeks())
            {
                sb.AppendLine(week.ToString(datesToHighlight));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Month"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Month"/>.</returns>
        public override string ToString() => ToString(null);
    }
}
