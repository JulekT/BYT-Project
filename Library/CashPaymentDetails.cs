namespace Library
{
    public class CashPaymentDetails : PaymentDetails
    {
        public Cashier ReceivedBy { get; }
        public double ChangeGiven { get; }

        public CashPaymentDetails(Cashier receivedBy, double changeGiven)
        {
            if (receivedBy == null)
                throw new ArgumentNullException(nameof(receivedBy));

            if (changeGiven < 0)
                throw new ArgumentException("Change given cannot be negative.");

            ReceivedBy = receivedBy;
            ChangeGiven = changeGiven;
        }
    }
}