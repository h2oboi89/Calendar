// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using Calendar.Library;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Calendar.Tests
{
    [TestFixture]
    public class UtilitiesTests
    {
        [Test]
        public void DayOfWeek_ReturnsCorrectDayofWeek()
        {
            var expected = new List<(DateTime date, DayOfWeek dayOfWeek)>
            {
                (new DateTime(1900, 1, 1), DayOfWeek.Monday),
                (new DateTime(2000, 1, 1), DayOfWeek.Saturday),
                (new DateTime(2021, 1, 1), DayOfWeek.Friday),
                (new DateTime(2021, 2, 1), DayOfWeek.Monday),
                (new DateTime(2021, 3, 1), DayOfWeek.Monday),
                (new DateTime(2021, 4, 1), DayOfWeek.Thursday),
                (new DateTime(2021, 5, 1), DayOfWeek.Saturday),
                (new DateTime(2021, 6, 1), DayOfWeek.Tuesday),
                (new DateTime(2021, 7, 1), DayOfWeek.Thursday),
                (new DateTime(2021, 8, 1), DayOfWeek.Sunday),
                (new DateTime(2021, 9, 1), DayOfWeek.Wednesday),
                (new DateTime(2021, 10, 1), DayOfWeek.Friday),
                (new DateTime(2021, 11, 1), DayOfWeek.Monday),
                (new DateTime(2021, 12, 1), DayOfWeek.Wednesday),
                (new DateTime(2100, 1, 1), DayOfWeek.Friday),
            };

            foreach(var (date, dayOfWeek) in expected)
            {
                Assert.That(Utilities.DayOfWeek(date), Is.EqualTo(dayOfWeek));
            }
        }
    }
}
