using System;
using Library;
using NUnit.Framework;

namespace Tests
{
    public class StaffInheritanceTests
    {
        [Test]
        public void Staff_CanReference_SalesPerson()
        {
            Staff staff = new SalesPerson(
                "Ali",
                DateTime.Now.AddYears(-1),
                3000,
                0.1
            );

            Assert.IsInstanceOf<SalesPerson>(staff);
        }

        [Test]
        public void SalesPerson_Inherits_Salary_From_Staff()
        {
            var startDate = DateTime.Now.AddYears(-2);

            Staff staff = new SalesPerson(
                "Veli",
                startDate,
                2000,
                0.2
            );

            double expectedSalary =
                2000 * (1 + Staff.YearlySalaryGrowthPercentage * 2);

            Assert.AreEqual(expectedSalary, staff.Salary);
        }

        [Test]
        public void Staff_CanBe_Manager_Or_Cashier()
        {
            Staff manager = new Manager(
                "Manager",
                DateTime.Now.AddYears(-3),
                5000
            );

            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            Staff cashier = new Cashier(
                "Cashier",
                DateTime.Now.AddYears(-1),
                2500,
                employmentType
            );

            Assert.IsInstanceOf<Manager>(manager);
            Assert.IsInstanceOf<Cashier>(cashier);
        }
    }
}