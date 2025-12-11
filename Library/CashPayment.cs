namespace Library
{
    public class CashPayment : Payment
    {
        private Cashier _receivedBy;
        private double _changeGiven;

        public Cashier ReceivedBy
        {
            get => _receivedBy;
            private set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(ReceivedBy), 
                        "A cashier must receive the payment.");
                _receivedBy = value;
            }
        }

        public double ChangeGiven
        {
            get => _changeGiven;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Change given cannot be negative.");
                _changeGiven = value;
            }
        }

        public CashPayment(
            int paymentID, 
            double amount, 
            DateTime date,
            Cashier receivedBy, 
            double changeGiven
        ) : base(paymentID, amount, date)
        {
            ReceivedBy = receivedBy;
            ChangeGiven = changeGiven;

            Payment.AddToExtent(this);
        }
    }
}