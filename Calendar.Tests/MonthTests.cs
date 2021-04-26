using Calendar.Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Tests
{
    [TestFixture]
    public class MonthTests
    {
        [Test]
        public void ZeroYear_ThrowsException()
        {
            Assert.That(() => new Month(0, 1), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With.Message.StartsWith("year must be greater than 0."));
        }

        [Test]
        public void OutOfRangeMonth_ThrowsException()
        {
            Assert.That(() => new Month(2000, 0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With.Message.StartsWith("month must be between 1 & 12."));

            Assert.That(() => new Month(2000, 13), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With.Message.StartsWith("month must be between 1 & 12."));
        }

        [Test]
        public void ValidArguments_ThrowNothing()
        {
            var month = new Month(2000, 1);

            Assert.That(month.Year, Is.EqualTo(2000));
            Assert.That(month.Value, Is.EqualTo(1));
            Assert.That(month.Length, Is.EqualTo(31));

            var expectedDate = new Date(2000, 1, 1);
            var actualDate = month[1];

            Assert.That(actualDate, Is.EqualTo(expectedDate));
            Assert.That(actualDate.DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void February_LeapYear_Has29Days()
        {
            var expected = new List<(int year, int days)>
            {
                (1999, 28), (2000, 29), (2001, 28)
            };

            foreach(var (year, days) in expected) {
                Assert.That(new Month(year, 2).Length, Is.EqualTo(days));
            }
        }

        [TestFixture]
        public class AsWeeksTests   
        {
            [Test]
            public void DefaultArguments_ReturnsMonthsAsWeeks()
            {
                var month = new Month(1998, 2);

                var weeks = month.AsWeeks().ToList();

                Assert.That(weeks.Count, Is.EqualTo(4));

                Assert.That(weeks[0][1], Is.EqualTo(new Date(1998, 2, 1)));
                Assert.That(weeks[3][7], Is.EqualTo(new Date(1998, 2, 28)));
            }

            [Test]
            public void WeekCount_PadsWeeks()
            {
                var month = new Month(1998, 2);

                var weeks = month.AsWeeks(6).ToList();

                Assert.That(weeks.Count, Is.EqualTo(6));
                
                for(var i = 4; i < 6; i++)
                {
                    for(var j = 0; j < 7; j++)
                    {
                        Assert.That(weeks[i][j + 1], Is.EqualTo(Date.Filler));
                    }
                }
            }
        }

        [Test]
        public void ToString_ReturnsString()
        {
            var month = new Month(1998, 2);
            var expected = string.Join(Environment.NewLine, new string[] {
                "      February      ",
                "--------------------",
                " S  M  T  W  T  F  S",
                " 1  2  3  4  5  6  7",
                " 8  9 10 11 12 13 14",
                "15 16 17 18 19 20 21",
                "22 23 24 25 26 27 28",
                ""
            });

            Assert.That(month.ToString(), Is.EqualTo(expected));
        }
    }
}
