using Library;
using NUnit.Framework;
using System.Reflection;
using System.Linq;

namespace Tests;

public class StaffTests
{
    [SetUp]
    public void Setup()
    {
        typeof(Staff)
            .GetField("_extent", BindingFlags.NonPublic | BindingFlags.Static)
            .SetValue(null, new List<Staff>());
    }

    [Test]
    public void Staff_Salary_IsDerivedCorrectly()
    {
        var startDate = DateTime.Now.AddYears(-2);
        var staff = new SalesPerson("Mert", startDate, 1000, 0.1);

        double expectedSalary =
            1000 * (1 + Staff.YearlySalaryGrowthPercentage * 2);

        Assert.AreEqual(expectedSalary, staff.Salary);
    }

    [Test]
    public void Staff_IsAddedToExtent_OnCreation()
    {
        int initialCount = Staff.Extent.Count;

        var staff = new SalesPerson("Ayşe", DateTime.Now, 3000, 0.2);

        Assert.AreEqual(initialCount + 1, Staff.Extent.Count);
        Assert.Contains(staff, Staff.Extent);
    }

    [Test]
    public void Staff_Extent_SaveAndLoad_Works()
    {
        var staff = new SalesPerson("Deniz", DateTime.Now, 4000, 0.15);

        Staff.SaveExtent("test_staff_extent.json");

        // Clear extent manually (correct way)
        typeof(Staff)
            .GetField("_extent", BindingFlags.NonPublic | BindingFlags.Static)
            .SetValue(null, new List<Staff>());

        Staff.LoadExtent("test_staff_extent.json");

        Assert.IsTrue(Staff.Extent.Any(s => s.Name == "Deniz"));
    }
}