using System;
using Library;
using NUnit.Framework;

namespace Tests
{
    public class PaymentInheritanceTests
    {
        [Test]
        public void Payment_CanReference_CashPayment()
        {
            Cashier cashier = new Cashier(
                "Cashier",
                DateTime.Now.AddYears(-1),
                2500,
                new FullTimeEmployment(20)
            );

            Payment payment = new CashPayment(
                1,
                100,
                DateTime.Now,
                cashier,
                0
            );

            Assert.IsInstanceOf<CashPayment>(payment);
            Assert.AreEqual(100, payment.Amount);
        }

        [Test]
        public void Payment_CanReference_CardPayment()
        {
            Payment payment = new CardPayment(
                2,
                250,
                DateTime.Now,
                "1234-5678-9012-3456",
                CardType.Visa
            );

            Assert.IsInstanceOf<CardPayment>(payment);
            Assert.AreEqual(250, payment.Amount);
        }
    }
}