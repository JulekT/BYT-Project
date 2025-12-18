using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CashPaymentDetailsTests
    {
        [Test]
        public void CashPaymentDetails_Creation_Works()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var cashier =
                new Cashier("Mert", DateTime.Now, 3000, employmentType);

            var details =
                new CashPaymentDetails(cashier, 10);

            Assert.AreEqual(cashier, details.ReceivedBy);
            Assert.AreEqual(10, details.ChangeGiven);
        }

        [Test]
        public void CashPaymentDetails_NullCashier_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CashPaymentDetails(null, 10);
            });
        }

        [Test]
        public void CashPaymentDetails_NegativeChange_Throws()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var cashier =
                new Cashier("Mert", DateTime.Now, 3000, employmentType);

            Assert.Throws<ArgumentException>(() =>
            {
                new CashPaymentDetails(cashier, -1);
            });
        }
        
        [Test]
        public void Payment_With_CashDetails_Works()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var cashier =
                new Cashier("Mert", DateTime.Now, 3000, employmentType);

            var details =
                new CashPaymentDetails(cashier, 10);

            var payment =
                new Payment(
                    1,
                    50,
                    DateTime.Now,
                    PaymentType.Cash,
                    details
                );

            Assert.AreEqual(PaymentType.Cash, payment.Type);
            Assert.AreSame(details, payment.Details);
        }

    }
}