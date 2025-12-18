namespace Library
{
    public enum CardType
    {
        Visa,
        Mastercard
    }

    public class CardPaymentDetails : PaymentDetails
    {
        public string CardNumber { get; }
        public CardType CardType { get; }

        public CardPaymentDetails(string cardNumber, CardType cardType)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty.");

            CardNumber = cardNumber;
            CardType = cardType;
        }
    }
}