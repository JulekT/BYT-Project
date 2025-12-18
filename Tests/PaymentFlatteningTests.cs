using System;
using Library;
using NUnit.Framework;

namespace Tests
{
    public class PaymentFlatteningTests
    {
        [Test]
        public void Payment_With_CashDetails_Is_CashType()
        {
            var employmentType =
                new EmploymentType(EmploymentTypeEnum.FullTime, 20);

            var cashier =
                new Cashier("Cashier", DateTime.Now.AddYears(-1), 2500, employmentType);

            var details =
                new CashPaymentDetails(cashier, 0);

            Payment payment =
                new Payment(
                    1,
                    100,
                    DateTime.Now,
                    PaymentType.Cash,
                    details
                );

            Assert.AreEqual(PaymentType.Cash, payment.Type);
            Assert.IsInstanceOf<CashPaymentDetails>(payment.Details);
            Assert.AreEqual(100, payment.Amount);
        }

        [Test]
        public void Payment_With_CardDetails_Is_CardType()
        {
            var details =
                new CardPaymentDetails(
                    "1234-5678-9012-3456",
                    CardType.Visa
                );

            Payment payment =
                new Payment(
                    2,
                    250,
                    DateTime.Now,
                    PaymentType.Card,
                    details
                );

            Assert.AreEqual(PaymentType.Card, payment.Type);
            Assert.IsInstanceOf<CardPaymentDetails>(payment.Details);
            Assert.AreEqual(250, payment.Amount);
        }
    }
}