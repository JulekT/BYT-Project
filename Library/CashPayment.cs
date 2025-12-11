namespace Library;

public class CashPayment : Payment
{
    private string _cardNumber;
    private CardType _cardType;

    public string CardNumber
    {
        get
        {
            if (string.IsNullOrEmpty(_cardNumber))
                throw new ValueNotAssigned("Card number is null, assign it first.");
            return _cardNumber;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Card number cannot be empty.");
            _cardNumber = value;
        }
    }

    public CardType CardType
    {
        get => _cardType;
        private set => _cardType = value;
    }

    public CashPayment(int paymentID, double amount, DateTime date, string cardNumber, CardType cardType)
        : base(paymentID, amount, date)
    {
        CardNumber = cardNumber;
        CardType = cardType;

        Payment.AddToExtent(this);
    }
}