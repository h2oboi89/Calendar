using Calendar.Library;
using CommandLine;
using System;
using System.Collections.Generic;

namespace Calendar
{
    class Program
    {
        class Options
        {
            [Option('y', "year", HelpText = "[int] Year to print calendar for (default is current year)")]
            public int Year { get; set; }

            [Option('m', "month", HelpText = "[int] 1-12. The month to print. Limits calendar to print just specified month.")]
            public int Month { get; set; }

            [Option('w', "width", HelpText = "[int] How many months wide the calendar is (default is 4)")]
            public int Width { get; set; }

            [Option('d', "date", HelpText = "[string] MM-dd. Date to highlight. Months part is optional (default is current month).")]
            public string Date { get; set; }

            [Option("today", HelpText = "[bool] If set today's date will be highlighted (default is false).")]
            public bool HighlightToday { get; set; }

            //[Option('s', "start", HelpText = "[int] Start month for range. 1 - 12 reference actual months. < 0 references offset from current month. Combine with 'count' option to display range of months.")]
            //public int Start { get; set; }

            //[Option('c', "count", HelpText = "[int] How many months to display. Only works if combined with 'start' option.")]
            //public int Count { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(Error);
        }

        static void Error(IEnumerable<Error> errors) {
            Environment.Exit(1);
        }

        static void Run(Options options)
        {
            var today = DateTime.Today;
            var datesToHighlight = new List<Date>();

            if (options.Year == default)
            {
                options.Year = today.Year;
            }

            if (options.Width == default)
            {
                options.Width = 4;
            }

            if (options.HighlightToday)
            {
                datesToHighlight.Add(new Date(today.Year, today.Month, today.Day));
            }

            if (options.Date != default)
            {
                try
                {
                    var parts = options.Date.Split('-');

                    if (parts.Length == 1)
                    {
                        var month = today.Month;

                        if (options.Month != default)
                        {
                            month = options.Month;
                        }

                        datesToHighlight.Add(new Date(options.Year, month, int.Parse(parts[0])));
                    }
                    else
                    {
                        datesToHighlight.Add(new Date(options.Year, int.Parse(parts[0]), int.Parse(parts[1])));
                    }
                }
                catch
                {
                    Console.WriteLine($"Invalid date format. Got '{options.Date}'. See help for usage.");
                    Environment.Exit(-1);
                }
            }

            var year = new Year(options.Year);

            if (options.Month == default)
            {
                Console.WriteLine(year.ToString(options.Width, datesToHighlight));
            }
            else
            {
                Console.WriteLine(year[options.Month].ToString(datesToHighlight));
            }
        }
    }
}
