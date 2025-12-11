using Library;
using NUnit.Framework;

namespace Tests
{
    public class ManagerReportTests
    {
        [Test]
        public void Manager_CreatesReport_Successfully()
        {
            var m = new Manager("Milo", DateTime.Now, 5000);

            var r = m.GenerateReport(ReportType.Sales, "Daily sales");

            Assert.AreEqual(m, r.Manager);
            Assert.Contains(r, m.Reports.ToList());
        }

        [Test]
        public void Manager_Destroy_RemovesReports()
        {
            var m = new Manager("Milo", DateTime.Now, 5000);

            var r1 = m.GenerateReport(ReportType.Sales);
            var r2 = m.GenerateReport(ReportType.Employee);

            m.Destroy();

            Assert.IsEmpty(m.Reports);
            Assert.IsFalse(Report.Extent.Contains(r1));
            Assert.IsFalse(Report.Extent.Contains(r2));
        }
    }
}