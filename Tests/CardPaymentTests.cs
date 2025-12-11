using Library;
using NUnit.Framework;

namespace Tests
{
    public class CardPaymentTests
    {
        [Test]
        public void CardPayment_Creation_Works()
        {
            var p = new CardPayment(1, 100, DateTime.Now, "1234", CardType.Visa);

            Assert.AreEqual("1234", p.CardNumber);
            Assert.AreEqual(CardType.Visa, p.CardType);
        }

        [Test]
        public void CardPayment_EmptyCardNumber_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CardPayment(1, 100, DateTime.Now, "", CardType.Visa);
            });
        }
    }
}