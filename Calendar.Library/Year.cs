using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Library
{
    /// <summary>
    /// Represents a year.
    /// </summary>
    public class Year
    { 
        private const int WEEK_COUNT = 6;
        private const int INTER_MONTH_SPACE = 2;

        /// <summary>
        /// Year value.
        /// </summary>
        public readonly int Value;

        private readonly Month[] Months;

        /// <summary>
        /// Number of months in the year (always 12).
        /// </summary>
        public int Length => Months.Length;

        /// <summary>
        /// Creates a new <see cref="Year"/> instance for the specified year value.
        /// </summary>
        /// <param name="year">Year to create an instance from.</param>
        public Year(int year)
        {
            if (year < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(year), $"{nameof(year)} must be greater than 0.");
            }

            Value = year;

            Months = new Month[Month.Names.Length];

            for (var i = 0; i < Months.Length; i++)
            {
                Months[i] = new Month(Value, i + 1);
            }
        }

        /// <summary>
        /// Gets specified <see cref="Month"/> from this <see cref="Year"/>.
        /// </summary>
        /// <param name="month">The one-based index of the month to retrieve.</param>
        /// <returns>The specified <see cref="Month"/> from this <see cref="Year"/>.</returns>
        public Month this[int month] => Months[month - 1];

        /// <summary>
        /// Checks if a specified year is a leap year.
        /// </summary>
        /// <param name="year">Year to check.</param>
        /// <returns><see langword="true"/> if year is a leap year; otherwise <see langword="false"/>.</returns>
        public static bool IsLeap(int year)
        {
            if (year % 400 == 0) return true;
            else if (year % 100 == 0) return false;
            else if (year % 4 == 0) return true;
            else return false;
        }

        private void GenerateRowOfMonths(ref int month, string[] group, List<string> output, int totalWidth, List<Date> datesToHighlight)
        {
            var monthNames = new List<string>();
            var weekHeaders = new List<string>();

            var weeks = new List<List<Week>>();

            foreach (var currentMonth in group)
            {
                if (currentMonth != null)
                {
                    monthNames.Add(currentMonth.Center(Month.WIDTH));
                    weekHeaders.Add(Week.HEADER);

                    weeks.Add(Months[month++].AsWeeks(WEEK_COUNT).ToList());
                }
            }

            output.Add(string.Join(' '.Repeat(INTER_MONTH_SPACE), monthNames));
            output.Add('-'.Repeat(totalWidth));
            output.Add(string.Join(' '.Repeat(INTER_MONTH_SPACE), weekHeaders));

            for (var i = 0; i < weeks[0].Count; i++)
            {
                var weekLine = new List<string>();

                for (var j = 0; j < weeks.Count; j++)
                {
                    weekLine.Add(weeks[j][i].ToString(datesToHighlight));
                }

                output.Add(string.Join(' '.Repeat(INTER_MONTH_SPACE), weekLine));
            }

            output.Add(' '.Repeat(totalWidth));
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Year"/>.
        /// Highlighting will be applied if any <see cref="Date"/> is in <paramref name="datesToHighlight"/>.
        /// </summary>
        /// <param name="width">How wide in months the year should be before wrapping.</param>
        /// <param name="datesToHighlight"><see cref="Date"/>s to add special highlighting to.</param>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Year"/>.</returns>
        public string ToString(int width, List<Date> datesToHighlight)
        {
            var totalWidth = (width * Month.WIDTH) + ((width - 1) * INTER_MONTH_SPACE);
            var output = new List<string>
            {
                '='.Repeat(totalWidth),
                string.Empty,
                Value.ToString().Center(totalWidth),
                string.Empty,
                '='.Repeat(totalWidth),
                string.Empty
            };

            var monthGroups = Month.Names.Split(width);

            var month = 0;

            foreach (var group in monthGroups)
            {
                GenerateRowOfMonths(ref month, group, output, totalWidth, datesToHighlight);
            }

            return string.Join(Environment.NewLine, output);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Year"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="Year"/>.</returns>
        public override string ToString() => ToString(1, null);
    }
}
