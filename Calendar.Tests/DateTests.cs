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
        public void Constructor_SetsFields()
        {
            var date = new Date(2000, 1, 2);

            Assert.That(date.Year, Is.EqualTo(2000));
            Assert.That(date.Month, Is.EqualTo(1));
            Assert.That(date.Day, Is.EqualTo(2));
        }

        [Test]
        public void Filler_HasFillerValues()
        {
            var date = Date.Filler;

            Assert.That(date.Year, Is.EqualTo(-1));
            Assert.That(date.Month, Is.EqualTo(-1));
            Assert.That(date.Day, Is.EqualTo(-1));
        }

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

        [TestFixture]
        public class EqualsAndGetHashCode
        {
            [Test]
            public void Null_ReturnsFalse()
            {
                var a = new Date(1, 2, 3);
                object b = null;

                Assert.That(a.Equals(b), Is.False);
            }

            [Test]
            public void NotDate_ReturnsFalse()
            {
                var a = new Date(1, 2, 3);
                var b = new object();

                Assert.That(a.Equals(b), Is.False);
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void Date_DifferentYear_ReturnsFalse()
            {
                var a = new Date(1, 2, 3);
                var b = new Date(4, 2, 3);

                Assert.That(a.Equals(b), Is.False);
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void Date_DifferentMonth_ReturnsFalse()
            {
                var a = new Date(1, 2, 3);
                var b = new Date(1, 4, 3);

                Assert.That(a.Equals(b), Is.False);
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void Date_DifferentDay_ReturnsFalse()
            {
                var a = new Date(1, 2, 3);
                var b = new Date(1, 2, 4);

                Assert.That(a.Equals(b), Is.False);
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void Date_SameValues_ReturnsTrue()
            {
                var a = new Date(1, 2, 3);
                var b = new Date(1, 2, 3);

                Assert.That(a.Equals(b), Is.True);
                Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void Date_SameReference_ReturnsTrue()
            {
                var a = new Date(1, 2, 3);
                var b = a;

                Assert.That(a.Equals(b), Is.True);
                Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            }
        }

        [TestFixture]
        public class _ToString
        {
            [Test]
            public void Filler_ReturnsBlankString()
            {
                var date = Date.Filler;

                Assert.That(date.ToString(), Is.EqualTo("  "));
                Assert.That(date.ToString(new List<Date>()), Is.EqualTo("  "));
            }

            [Test]
            public void Date_ReturnsDateString()
            {
                var date = new Date(1, 2, 3);

                Assert.That(date.ToString(), Is.EqualTo(" 3"));
                Assert.That(date.ToString(new List<Date>()), Is.EqualTo(" 3"));
            }

            [Test]
            public void Date_Highlighted_ReturnsCodedString()
            {
                var date = new Date(1, 2, 3);

                var datesToHighlight = new List<Date> { new Date(3, 2, 1) };

                Assert.That(date.ToString(datesToHighlight), Is.EqualTo(" 3"));

                datesToHighlight.Add(date);

                Assert.That(date.ToString(datesToHighlight), Is.EqualTo("\u001b[7m 3\u001b[0m"));
            }
        }
    }
}
