using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CashPaymentTests
    {
        [Test]
        public void CashPayment_Creation_Works()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var cashier =
                new Cashier("Mert", DateTime.Now, 3000, employmentType);

            var p =
                new CashPayment(1, 50, DateTime.Now, cashier, 10);

            Assert.AreEqual(cashier, p.ReceivedBy);
            Assert.AreEqual(10, p.ChangeGiven);
        }

        [Test]
        public void CashPayment_NullCashier_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CashPayment(1, 50, DateTime.Now, null, 10);
            });
        }

        [Test]
        public void CashPayment_NegativeChange_Throws()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var cashier =
                new Cashier("Mert", DateTime.Now, 3000, employmentType);

            Assert.Throws<ArgumentException>(() =>
            {
                new CashPayment(1, 50, DateTime.Now, cashier, -1);
            });
        }
    }
}