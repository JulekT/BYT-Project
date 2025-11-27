using System;
using Library; 
public class CardPayment : Payment
{
	public enum CardType
	{
		Visa = 0,
		Mastercard = 1
	}
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
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("Card number can't be empty");
            _cardNumber = value;
        }
    }
    public CardPayment()
	{
	}
}
