using System;
using System.Collections.Generic;

namespace Calendar.Library
{
    /// <summary>
    /// Represents a date in time.
    /// </summary>
    public class Date
    {
        private const int FILLER_VALUE = -1;

        /// <summary>
        /// Width in <see cref="char"/> when converted to a <see cref="string"/>.
        /// </summary>
        public const int WIDTH = 2;

        /// <summary>
        /// Filler <see cref="Date"/> when printing blank spaces before fist and after last day of month.
        /// </summary>
        public static readonly Date Filler = new Date();

        /// <summary>
        /// Year associated with this <see cref="Date"/> [1 - 9999].
        /// </summary>
        public readonly int Year;

        /// <summary>
        /// Month associated with this <see cref="Date"/> [1 - 12].
        /// </summary>
        public readonly int Month;

        /// <summary>
        /// Day associated with this <see cref="Date"/> [1 - End of Month] (depends on month).
        /// </summary>
        public readonly int Day;

        private Date() : this(FILLER_VALUE, FILLER_VALUE, FILLER_VALUE) { }

        /// <summary>
        /// Creates a new <see cref="Date"/> instance for the specified date.
        /// </summary>
        /// <param name="year">Year value.</param>
        /// <param name="month">Month value.</param>
        /// <param name="day">Day value.</param>
        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// Determines if the specified <see cref="object"/> is equal is equal to the current <see cref="Date"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Date"/>.</param>
        /// <returns><see langword="true"/> if the specified <see cref="object"/> is equal to the current <see cref="Date"/>; otherwise <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Date date)
            {
                if (ReferenceEquals(this, date)) {
                    return true;
                }

                return Year == date.Year
                    && Month == date.Month
                    && Day == date.Day;
            }

            return false;
        }

        /// <summary>
        /// Retuns the hashcode for this instance.
        /// </summary>
        /// <returns>An <see cref="int"/> hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 592158470;
            hashCode = hashCode * -1521134295 + Year.GetHashCode();
            hashCode = hashCode * -1521134295 + Month.GetHashCode();
            hashCode = hashCode * -1521134295 + Day.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Date"/>.
        /// Highlighting will be applied if current <see cref="Date"/> is in <paramref name="datesToHighlight"/>.
        /// </summary>
        /// <param name="datesToHighlight"><see cref="Date"/>s to add special highlighting to.</param>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Date"/>.</returns>
        public string ToString(List<Date> datesToHighlight)
        {
            var value = ToString();

            if (Equals(Filler) || datesToHighlight == null)
            {
                return value;
            }

            foreach (var date in datesToHighlight)
            {
                if (Equals(date))
                {
                    // This inverts console foreground and background colors to highlight value
                    value = $"\u001b[7m{value}\u001b[0m";
                    break;
                }
            }

            return value;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Date"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Date"/>.</returns>
        public override string ToString() => Day == FILLER_VALUE
            ? ' '.Repeat(WIDTH)
            : Day.ToString().PadLeft(WIDTH);

        /// <summary>
        /// Determines which day of the week (Sunday, Monday, ...) a this <see cref="Date"/> falls on.
        /// Based on <see href="https://en.wikipedia.org/wiki/Determination_of_the_day_of_the_week#Zeller's_algorithm">Zeller's Algorithm</see>.
        /// </summary>
        /// <returns><see cref="System.DayOfWeek"/> this <see cref="Date"/> falls on.</returns>
        public DayOfWeek DayOfWeek
        {
            get
            {
                var month = Month < 3 ? Month + 12 : Month;

                var year = Year;

                if (month > 12)
                {
                    year -= 1;
                }

                var century = year / 100;

                year %= 100;

                var a = Day;
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
}
