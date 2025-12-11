namespace Library;
public enum CardType
{
    Visa = 0,
    Mastercard = 1
}
public class CardPayment : Payment
{
    
    private string _cardNumber;
    private CardType _cardType;
    public string CardNumber
    {
        get
        {
            if (String.IsNullOrEmpty(_cardNumber))
                throw new ValueNotAssigned("Card number is null, you need to assign it firstly");
            return _cardNumber;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Card number can't be empty");
            _cardNumber = value;
        }
    }
    public CardType CardType { get; private set; }
    public CardPayment(int paymentID, double amount, DateTime date, string cardNumber, CardType cardType)
        : base(paymentID, amount, date) 
    {
        CardNumber = cardNumber;
        CardType = cardType;
    }

}