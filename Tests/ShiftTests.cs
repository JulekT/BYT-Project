using Library;
using NUnit.Framework;

namespace Tests
{
    public class ShiftTests
    {
        [Test]
        public void Shifts_NotConflicting_WhenDifferentDays()
        {
            var s1 = new Shift(DateTime.Today, DateTime.Today.AddHours(9), DateTime.Today.AddHours(17));
            var s2 = new Shift(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1).AddHours(9), DateTime.Today.AddDays(1).AddHours(17));

            Assert.IsFalse(s1.ConflictsWith(s2));
        }

        [Test]
        public void Shifts_Conflict_WhenTimeOverlaps()
        {
            var s1 = new Shift(DateTime.Today, DateTime.Today.AddHours(9), DateTime.Today.AddHours(17));
            var s2 = new Shift(DateTime.Today, DateTime.Today.AddHours(13), DateTime.Today.AddHours(20));

            Assert.IsTrue(s1.ConflictsWith(s2));
        }

        [Test]
        public void Shifts_NoConflict_WhenAdjacent()
        {
            var s1 = new Shift(DateTime.Today, DateTime.Today.AddHours(9), DateTime.Today.AddHours(12));
            var s2 = new Shift(DateTime.Today, DateTime.Today.AddHours(12), DateTime.Today.AddHours(17));

            Assert.IsFalse(s1.ConflictsWith(s2));
        }

        [Test]
        public void Shift_InvalidStart_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Shift(DateTime.Today, DateTime.MinValue, DateTime.Today.AddHours(17));
            });
        }

        [Test]
        public void Shift_EndBeforeStart_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Shift(DateTime.Today, DateTime.Today.AddHours(12), DateTime.Today.AddHours(11));
            });
        }
    }
}