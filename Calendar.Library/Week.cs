using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Library
{
    /// <summary>
    /// Represents a period of 7 <see cref="Date"/>s (starting on Sunday) inside of a <see cref="Month"/>.
    /// </summary>
    public class Week : IEquatable<Week>
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

            foreach (var day in days)
            {
                if (day == null)
                {
                    throw new ArgumentException($"{nameof(days)} can't contain null {typeof(Date)}s.", nameof(days));
                }
            }

            Validate(days);
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
            if (days == null)
            {
                throw new ArgumentNullException(nameof(days));
            }

            if (days.Count == 0)
            {
                throw new ArgumentException("No days to form week.", nameof(days));
            }

            var weekDays = Empty.Days.Clone<Date>();

            var prev = -1;

            while (days.Count > 0)
            {
                var day = days.Peek();

                var dayOfWeek = (int)day.DayOfWeek;

                if (dayOfWeek < prev)
                {
                    break;
                }

                day = days.Dequeue();

                weekDays[dayOfWeek] = day;

                prev = dayOfWeek;
            }

            Validate(weekDays);

            return new Week(weekDays);
        }

        private static void Validate(Date[] days)
        {
            var fillerSections = days.FindSections()
                .Where(s => days[s.start].Equals(Date.Filler))
                .ToList();

            if (fillerSections.Count > 1)
            {
                throw new ArgumentException($"Only 1 section of date fillers is allowed, got {fillerSections.Count}.", nameof(days));
            }

            if (fillerSections.Count > 0)
            {
                var fillerSection = fillerSections[0];
                var start = 0;
                var end = days.Length - 1;

                if ((fillerSection.start != start && fillerSection.end != end) ||
                    (fillerSection.end != end && fillerSection.start != start))
                {
                    throw new ArgumentException("Filler sections must be at start or end.", nameof(days));
                }
            }
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

        /// <summary>
        /// Determines if the specified <see cref="object"/> is equal is equal to the current <see cref="Week"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Week"/>.</param>
        /// <returns><see langword="true"/> if the specified <see cref="object"/> is equal to the current <see cref="Week"/>; otherwise <see langword="false"/>.</returns>
        public override bool Equals(object obj) => Equals(obj as Week);

        /// <summary>
        /// Determines if the specified <see cref="Week"/> is equal is equal to the current <see cref="Week"/>.
        /// </summary>
        /// <param name="other">The <see cref="Week"/> to compare with the current <see cref="Week"/>.</param>
        /// <returns><see langword="true"/> if the specified <see cref="Week"/> is equal to the current <see cref="Week"/>; otherwise <see langword="false"/>.</returns>
        public bool Equals(Week other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            for (var i = 0; i < LENGTH; i++)
            {
                if (Days[i] != other.Days[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if two <see cref="Week"/>s are equal.
        /// </summary>
        /// <param name="lhs">Left hand operand.</param>
        /// <param name="rhs">Right hand operand.</param>
        /// <returns><see langword="true"/> if operands are equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(Week lhs, Week rhs)
        {
            if (lhs is null)
            {
                return rhs is null;
            }

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines if two <see cref="Week"/>s are not equal.
        /// </summary>
        /// <param name="lhs">Left hand operand.</param>
        /// <param name="rhs">Right hand operand.</param>
        /// <returns><see langword="true"/> if operands are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(Week lhs, Week rhs) => !(lhs == rhs);

        /// <summary>
        /// Retuns the hashcode for this instance.
        /// </summary>
        /// <returns>An <see cref="int"/> hash code.</returns>
        public override int GetHashCode()
        {
            var hashCode = 339055328;

            foreach (var date in Days)
            {
                hashCode = hashCode * -1521134295 + date.GetHashCode();
            }

            return hashCode;
        }
    }
}
