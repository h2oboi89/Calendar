using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Library
{
    public static class Utilities
    {
        private static int Floor(double value)
        {
            return (int)Math.Floor(value);
        }

        private static int Modulo(int value, int mod)
        {
            return ((value % mod) + mod) % mod;
        }

        public static DayOfWeek DayOfWeek(DateTime date)
        {
            var day = date.Day;
            var month = date.Month < 3 ? date.Month + 12 : date.Month;

            var year = date.Year;

            if (month > 12)
            {
                year -= 1;
            }

            var century = year / 100;

            year %= 100;

            var a = day;
            var b = Floor(13.0 * (month + 1) / 5);
            var c = year;
            var d = Floor(year / 4.0);
            var e = Floor(century / 4.0);
            var f = -2 * century;

            var dayOfWeek = Modulo(a + b + c + d + e + f, 7);

            dayOfWeek = (dayOfWeek + 6) % 7;

            return (DayOfWeek)dayOfWeek;
        }
    }
}
