using Library;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class StaffTests
    {
        [SetUp]
        public void Setup()
        {
            Staff.Extent = new List<Staff>();
        }

        [Test]
        public void Salary_IsCalculated_FromBaseSalary_WhenCreated()
        {
            var startDate = new DateTime(2020, 1, 1);
            var staff = new SalesPerson("Mert", startDate, 1000, 0.1);

            int years =
                (int)((DateTime.Now - startDate).TotalDays / 365);

            double expectedSalary =
                1000 * (1 + Staff.YearlySalaryGrowthPercentage * years);

            Assert.That(staff.Salary, Is.EqualTo(expectedSalary));
        }

        [Test]
        public void Staff_IsAddedToExtent_WhenConcreteStaffIsCreated()
        {
            int initialCount = Staff.Extent.Count;

            var staff = new Manager(
                "Ayşe",
                new DateTime(2021, 5, 1),
                3000
            );

            Assert.AreEqual(initialCount + 1, Staff.Extent.Count);
            Assert.IsTrue(Staff.Extent.Contains(staff));
        }

        [Test]
        public void DifferentStaffTypes_CanCoexist_InExtent()
        {
            var s1 = new SalesPerson(
                "Ali",
                new DateTime(2020, 1, 1),
                2000,
                0.1
            );

            var s2 = new Manager(
                "Deniz",
                new DateTime(2019, 3, 10),
                4000
            );

            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 18);

            var s3 = new Cashier(
                "Zeynep",
                new DateTime(2022, 6, 1),
                1800,
                employmentType
            );

            var extent = Staff.Extent;

            Assert.AreEqual(3, extent.Count);
            Assert.IsTrue(extent.OfType<SalesPerson>().Any());
            Assert.IsTrue(extent.OfType<Manager>().Any());
            Assert.IsTrue(extent.OfType<Cashier>().Any());
        }

        [Test]
        public void Staff_CanHave_Manager_AssignedCorrectly()
        {
            var manager = new Manager(
                "Manager",
                new DateTime(2018, 1, 1),
                5000
            );

            var staff = new SalesPerson(
                "Employee",
                new DateTime(2022, 1, 1),
                2000,
                0.1
            );

            staff.AddManager(manager);

            Assert.AreEqual(manager, staff.Manager);
            Assert.IsTrue(manager.ManagedStaff.Contains(staff));
        }
    }
}
