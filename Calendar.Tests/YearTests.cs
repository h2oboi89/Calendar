using Calendar.Library;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Tests
{
    [TestFixture]
    public class YearTests
    {
        [Test]
        public void ZeroYear_ThrowsException()
        {
            Assert.That(() => new Year(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                .With.Message.StartsWith("year must be greater than 0."));
        }

        [Test]
        public void ValidArguments_ThrowNothing()
        {
            var year = new Year(2000);

            Assert.That(year.Value, Is.EqualTo(2000));
            Assert.That(year.Length, Is.EqualTo(12));

            var expected = new List<(int month, int days)>
            {
                (1, 31), (2, 29), (3, 31), (4, 30),
                (5, 31), (6, 30), (7, 31), (8, 31),
                (9, 30), (10, 31), (11, 30), (12, 31)
            };

            foreach (var (month, days) in expected)
            {
                Assert.That(year[month].Length, Is.EqualTo(days));
            }
        }

        [Test]
        public void IsLeapYear_HasCorrectValues()
        {
            var expected = new List<(int year, bool isLeap)>
            {
                (1896, true), (1900, false), (1904, true), (2000, true), (2020, true)
            };

            foreach(var (year, isLeap) in expected)
            {
                Assert.That(Year.IsLeap(year), Is.EqualTo(isLeap));
            }
        }

        [Test]
        public void ToString_SpecifiedWidth_PrintsCalendar()
        {
            var directory = Path.Combine(
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(YearTests)).Location),
                "TestFiles"
            );

            for (var i = 1; i < 13; i++)
            {
                var file = Path.Combine(directory, $"w{i}.txt");

                var expected = File.ReadAllText(file);
                var actual = $"{new Year(2000).ToString(i, null)}{Environment.NewLine}";

                Assert.That(actual, Is.EqualTo(expected));
            }            
        }

        [Test]
        public void ToString_Default_PrintsCalendar()
        {
            var file = Path.Combine(
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(YearTests)).Location),
                "TestFiles",
                "w1.txt"
            );

            var expected = File.ReadAllText(file);
            var actual = $"{new Year(2000)}{Environment.NewLine}";

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
