using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Library
{
    /// <summary>
    /// Represents a period of 7 <see cref="Date"/>s (starting on Sunday) inside of a <see cref="Month"/>.
    /// </summary>
    public class Week
    {
        private const int LENGTH = 7;
        private static readonly string[] HEADER_VALUES = new string[] { "S", "M", "T", "W", "T", "F", "S" };

        /// <summary>
        /// Formatted header value when printing a <see cref="Month"/>.
        /// </summary>
        public static readonly string HEADER = string.Join(" ", HEADER_VALUES.Select(v => v.PadLeft(Date.WIDTH)));

        /// <summary>
        /// Filler week to even out when printing multiple <see cref="Month"/>s in a row.
        /// Some <see cref="Month"/>s will cross into 6 weeks while others will have less.
        /// </summary>
        public static readonly Week Empty = new Week(new Date[LENGTH].Fill(Date.Filler));

        private readonly Date[] Days;

        /// <summary>
        /// Creates a new instance with the specified days.
        /// </summary>
        /// <param name="days">Days that will make up the week.</param>
        public Week(Date[] days)
        {
            Days = days ?? throw new ArgumentNullException(nameof(days));

            if (Days.Length != LENGTH)
            {
                throw new ArgumentException($"Invalid {nameof(days)}. Should be length {LENGTH}.", nameof(days));
            }

            foreach(var day in days)
            {
                if (day == null)
                {
                    throw new ArgumentException($"{days} can't contain null {typeof(Date)}s.", nameof(days));
                }
            }
        }

        /// <summary>
        /// Gets specified <see cref="Date"/> from this <see cref="Week"/>.
        /// </summary>
        /// <param name="day">The one-based index of the day to retrieve.</param>
        /// <returns>The specified <see cref="Date"/> from this <see cref="Week"/>.</returns>
        public Date this[int day] => Days[day - 1];

        /// <summary>
        /// Creates a new <see cref="Week"/> from the specified values.
        /// </summary>
        /// <param name="days">Days in the current month to create a week from.</param>
        /// <returns>Next <see cref="Week"/> from <paramref name="days"/>.</returns>
        public static Week Generate(Queue<Date> days)
        {
            var firstDay = (int)days.Peek().DayOfWeek;

            var weekDays = Empty.Days.Clone<Date>();

            for (var i = firstDay; days.Count > 0 && i < LENGTH; i++)
            {
                weekDays[i] = days.Dequeue();
            }

            return new Week(weekDays);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Week"/>.
        /// Highlighting will be applied if any <see cref="Date"/> is in <paramref name="datesToHighlight"/>.
        /// </summary>
        /// <param name="datesToHighlight"><see cref="Week"/>s to add special highlighting to.</param>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Week"/>.</returns>
        public string ToString(List<Date> datesToHighlight)
        {
            return string.Join(" ", Days.Select(d => d.ToString(datesToHighlight)));
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Week"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Week"/>.</returns>
        public override string ToString() => ToString(null);
    }
}
