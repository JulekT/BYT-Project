using Library;
using NUnit.Framework;

namespace Tests
{
    public class ReportTests
    {
        [Test]
        public void Report_Creates_Properly()
        {
            var manager = new Manager("Alice", DateTime.Now, 4000);
            var report = new Report(manager, ReportType.Sales, "Daily overview");

            Assert.AreEqual(manager, report.Manager);
            Assert.AreEqual(ReportType.Sales, report.Type);
            Assert.AreEqual("Daily overview", report.Description);
        }

        [Test]
        public void Report_MustHaveManager()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Report(null, ReportType.Sales);
            });
        }
    }
}