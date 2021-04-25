using Calendar.Library;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Tests
{
    [TestFixture]
    public class WeekTests
    {
        private static Date[] MakeDays(int year, int month, int day, int count)
        {
            var days = new Date[count];

            for (var i = 0; i < count; i++)
            {
                days[i] = new Date(year, month, day + i);
            }

            return days;
        }

        [TestFixture]
        public class ConstructorTests
        {
            [Test]
            public void NullArgs_ThrowsException()
            {
                Assert.That(() => new Week(null), Throws.ArgumentNullException);
            }

            [Test]
            public void InvalidLength_ThrowsException()
            {
                var days = new Date[5];

                Assert.That(() => new Week(new Date[5]), Throws.ArgumentException.With.Message.StartsWith("Invalid days. Should be length"));
            }

            [Test]
            public void NullDates_ThrowsException()
            {
                var days = new Date[7];

                for (var i = 0; i < days.Length - 1; i++)
                {
                    Assert.That(() => new Week(days), Throws.ArgumentException.With.Message.StartsWith("days can't contain null"));
                    days[i] = Date.Filler;
                }
            }

            [Test]
            public void ValidDates_DoesNotThrow()
            {
                var days = new Date[7];

                for (var i = 0; i < days.Length; i++)
                {
                    days[i] = new Date(2000, 1, i + 1);
                }

                var week = new Week(days);

                for (var i = 0; i < 7; i++)
                {
                    Assert.That(week[i + 1], Is.EqualTo(days[i]));
                }
            }

        }
        [TestFixture]
        public class GenerateTests
        {

            [Test]
            public void NullArg_ThrowsException()
            {
                Assert.That(() => Week.Generate(null), Throws.ArgumentNullException);
            }

            [Test]
            public void ZeroDays_ThrowsException()
            {
                Assert.That(() => Week.Generate(new Date[0].ToQueue()), Throws.ArgumentException.With.Message.StartsWith("No days to form week."));
            }

            [Test]
            public void PartialFirstWeek_IsValid()
            {
                var filler = new Date[] { Date.Filler, Date.Filler };

                var days = MakeDays(2000, 2, 1, 5);

                var expectedWeek = new Week(filler.Concat(days));

                var actualWeek = Week.Generate(days.ToQueue());

                Assert.That(actualWeek.Equals(expectedWeek), Is.True);
            }

            [Test]
            public void FullWeek_IsValid()
            {
                var days = MakeDays(2000, 2, 13, 7);

                var expectedWeek = new Week(days);

                var actualWeek = Week.Generate(days.ToQueue());

                Assert.That(actualWeek.Equals(expectedWeek), Is.True);
            }

            [Test]
            public void PartialLastWeek_IsValid()
            {
                var filler = new Date[] { Date.Filler, Date.Filler, Date.Filler, Date.Filler };

                var days = MakeDays(2000, 2, 27, 3);

                var expectedWeek = new Week(days.Concat(filler));

                var actualWeek = Week.Generate(days.ToQueue());

                Assert.That(actualWeek.Equals(expectedWeek), Is.True);
            }

            [Test]
            public void FillerInMiddle_IsInvalid()
            {
                var startDays = MakeDays(2000, 2, 6, 2);
                var endDays = MakeDays(2000, 2, 11, 2);

                Assert.That(() => Week.Generate(startDays.Concat(endDays).ToQueue()), Throws.ArgumentException.With.Message.StartsWith("Filler sections must be at start or end."));
            }

            [Test]
            public void FillerInBothEnds_IsInvalid()
            {
                var days = MakeDays(2000, 2, 8, 3);

                Assert.That(() => Week.Generate(days.ToQueue()), Throws.ArgumentException.With.Message.StartsWith("Only 1 section of date fillers is allowed, got 2."));
            }

            [Test]
            public void AllFillerExceptLast_IsValid()
            {
                var days = MakeDays(2021, 1, 1, 31);
                var filler = new Date[5].Fill(Date.Filler);

                var expectedWeek = new Week(filler.Concat(days.GetRange(0, 2)));

                var actualWeek = Week.Generate(days.ToQueue());

                Assert.That(actualWeek.Equals(expectedWeek), Is.True);
            }
        }

        [Test]
        public void ToString_ReturnsString()
        {
            var days = MakeDays(2000, 2, 1, 6);

            var week = Week.Generate(days.ToQueue());

            Assert.That(week.ToString(), Is.EqualTo("       1  2  3  4  5"));
        }

        [TestFixture]
        public class EqualsAndGetHashCodeTests
        {
            [Test]
            public void Null_ReturnsFalse()
            {
                var a = Week.Generate(MakeDays(2000, 2, 1, 6).ToQueue());
                Week b = null;

                Assert.That(a.Equals(b), Is.False);
            }

            [Test]
            public void NotWeek_ReturnsFalse()
            {
                var a = Week.Generate(MakeDays(2000, 2, 1, 6).ToQueue());
                var b = new object();

                Assert.That(a.Equals(b), Is.False);
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void DifferentDates_ReturnsFalse()
            {
                var a = Week.Generate(MakeDays(2000, 2, 1, 7).ToQueue());
                var b = Week.Generate(MakeDays(2021, 2, 1, 7).ToQueue());

                Assert.That(a.Equals(b), Is.False);
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void SameDates_ReturnsTrue()
            {
                var a = Week.Generate(MakeDays(2000, 2, 1, 7).ToQueue());
                var b = Week.Generate(MakeDays(2000, 2, 1, 7).ToQueue());

                Assert.That(a.Equals(b), Is.True);
                Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void SameReference_ReturnsTrue()
            {
                var a = Week.Generate(MakeDays(2000, 2, 1, 7).ToQueue());
                var b = a;

                Assert.That(a.Equals(b), Is.True);
                Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            }

            [Test]
            public void EqualityOperators_WorkAsExpected()
            {
                var a = Week.Generate(MakeDays(2000, 2, 1, 7).ToQueue());
                var b = Week.Generate(MakeDays(2000, 2, 1, 7).ToQueue());
                var c = Week.Generate(MakeDays(2021, 2, 1, 7).ToQueue());

                Assert.That(a == b, Is.True);
                Assert.That(a == c, Is.False);

                Assert.That(a != c, Is.True);
                Assert.That(a != b, Is.False);

                Assert.That(null == (Week)null, Is.True);
                Assert.That(null == a, Is.False);
            }
        }
    }
}
