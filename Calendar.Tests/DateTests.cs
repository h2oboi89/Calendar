// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using Calendar.Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Calendar.Tests
{
    [TestFixture]
    public class DateTests
    {
        [Test]
        public void DayOfWeek_ReturnsCorrectDayofWeek()
        {

            var expected = new List<(int year, int month, int day, DayOfWeek dayOfWeek)>
            {
                (1900, 1, 1, DayOfWeek.Monday),
                (2000, 1, 1, DayOfWeek.Saturday),
                (2021, 1, 1, DayOfWeek.Friday),
                (2021, 2, 1, DayOfWeek.Monday),
                (2021, 3, 1, DayOfWeek.Monday),
                (2021, 4, 1, DayOfWeek.Thursday),
                (2021, 5, 1, DayOfWeek.Saturday),
                (2021, 6, 1, DayOfWeek.Tuesday),
                (2021, 7, 1, DayOfWeek.Thursday),
                (2021, 8, 1, DayOfWeek.Sunday),
                (2021, 9, 1, DayOfWeek.Wednesday),
                (2021, 10, 1, DayOfWeek.Friday),
                (2021, 11, 1, DayOfWeek.Monday),
                (2021, 12, 1, DayOfWeek.Wednesday),
                (2100, 1, 1, DayOfWeek.Friday),
            };

            foreach (var (year, month, day, dayOfWeek) in expected)
            {
                Assert.That(new Date(year, month, day).DayOfWeek, Is.EqualTo(dayOfWeek));
            }
        }
    }
}
