using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CashierEmploymentTests
    {
        [Test]
        public void Cashier_CannotHaveNullEmploymentType()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Cashier(
                    "Mert",
                    DateTime.Now,
                    3000,
                    null
                );
            });
        }

        [Test]
        public void Cashier_CanChangeEmploymentType()
        {
            var fullTime =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var partTime =
                new EmploymentType(EmploymentTypeEnum.PartTime, 15);

            var cashier = new Cashier(
                "Mert",
                DateTime.Now,
                3000,
                fullTime
            );

            cashier.ChangeEmploymentType(partTime);

            Assert.AreEqual(partTime, cashier.EmploymentType);
            Assert.AreEqual(
                EmploymentTypeEnum.PartTime,
                cashier.EmploymentType.Type
            );
        }
    }
}