using Calendar.Library;
using CommandLine;
using System;
using System.Diagnostics;

namespace Calendar
{
    class Program
    {
        class Options
        {
            [Option('y', "year", HelpText = "[int] Year to print calendar for (default is current year)")]
            public int Year { get; set; }

            [Option('w', "width", HelpText = "[int] How many months wide the calendar is (default is 4)")]
            public int Width { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run);
        }

        static void Run(Options options)
        {
            if (options.Year == default)
            {
                options.Year = DateTime.Now.Year;
            }

            if (options.Width == default)
            {
                options.Width = 4;
            }

            var lines = Utilities.Generate(options.Year, options.Width);

            Console.WriteLine(string.Join(Environment.NewLine, lines));

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }
}
