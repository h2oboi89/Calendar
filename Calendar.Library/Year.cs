using System.Collections.Generic;

namespace Calendar.Library
{
    public class Year
    {
        public static bool IsLeap(int year)
        {
            if (year % 400 == 0) return true;
            else if (year % 100 == 0) return false;
            else if (year % 4 == 0) return true;
            else return false;
        }


        private const int INTER_MONTH_SPACE = 2;

        private static void GenerateRowOfMonths(int year, ref int month, string[] group, List<string> output, int totalWidth)
        {
            var monthNames = new List<string>();
            var weekHeaders = new List<string>();

            var weeks = new List<List<string>>();

            foreach (var currentMonth in group)
            {
                if (currentMonth != null)
                {
                    monthNames.Add(currentMonth.Center(Month.WIDTH));
                    weekHeaders.Add(Week.HEADER);

                    weeks.Add(Month.Generate(year, month++));
                }
            }

            // use queue to cycle through weeks?
            weeks.Equalize(' '.Repeat(Week.HEADER.Length));

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
            var totalWidth = (width * Month.WIDTH) + ((width - 1) * INTER_MONTH_SPACE);
            var output = new List<string>
            {
                '='.Repeat(totalWidth),
                string.Empty,
                year.ToString().Center(totalWidth),
                string.Empty,
                '='.Repeat(totalWidth),
                string.Empty
            };

            var monthGroups = Month.Names.Split(width);

            var month = 0;

            foreach (var group in monthGroups)
            {
                GenerateRowOfMonths(year, ref month, group, output, totalWidth);
            }

            return output;
        }
    }
}
