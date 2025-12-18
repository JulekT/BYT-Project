using Library;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CardPaymentDetailsTests
    {
        [Test]
        public void CardPaymentDetails_Creation_Works()
        {
            var details = new CardPaymentDetails(
                "1234",
                CardType.Visa
            );

            Assert.AreEqual("1234", details.CardNumber);
            Assert.AreEqual(CardType.Visa, details.CardType);
        }

        [Test]
        public void CardPaymentDetails_EmptyCardNumber_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new CardPaymentDetails(
                    "",
                    CardType.Visa
                );
            });
        }
        [Test]
        public void Payment_With_CardDetails_Works()
        {
            var details = new CardPaymentDetails(
                "9999",
                CardType.Mastercard
            );

            var payment = new Payment(
                1,
                250,
                DateTime.Now,
                PaymentType.Card,
                details
            );

            Assert.AreEqual(PaymentType.Card, payment.Type);
            Assert.AreSame(details, payment.Details);
        }

    }
}