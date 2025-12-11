
using System.Text.Json;

namespace Library
{
    public abstract class Payment
    {
        private static List<Payment> _extent = new();
        public static List<Payment> Extent
        {
            get => _extent;
            private set => _extent = value ?? new List<Payment>();
        }

        public static void AddToExtent(Payment p)
        {
            if (p == null) throw new ArgumentException("Payment cannot be null");
            _extent.Add(p);
        }
        
        private int _paymentID;
        private double _amount;
        private DateTime _date;

        public int PaymentID
        {
            get
            {
                if (_paymentID < 0)
                    throw new NumberIsNotPositive("ID must be positive");
                return _paymentID;
            }
            set
            {
                if (value < 0)
                    throw new NumberIsNotPositive("ID must be positive");
                _paymentID = value;
            }
        }
        public double Amount
        {
            get
            {
                if (_amount < 0)
                    throw new NumberIsNotPositive("ID must be positive");
                return _amount;
            }
            set
            {
                if (value < 0)
                    throw new NumberIsNotPositive("ID must be positive");
                _amount = value;
            }
        }
        public DateTime Date
        {
            get
            {
                if (_date == DateTime.MinValue)
                    throw new ValueNotAssigned("Payment date is null");
                return _date;
            }
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentNullException("Date can't be empty");
                _date = value;
            }
        }

        public Payment() { }

        public Payment(int paymentID, double amount, DateTime date)
        {
            _paymentID = paymentID;
            _amount = amount;
            _date = date;
        }

        public static void SaveExtent(string fileName = "payment_extent.json")
        {
            var json = JsonSerializer.Serialize(
                _extent,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(fileName, json);
        }

        public static void LoadExtent(string fileName = "payment_extent.json")
        {
            if (!File.Exists(fileName))
                return;

            var json = File.ReadAllText(fileName);
            _extent = JsonSerializer.Deserialize<List<Payment>>(json);
        }

    }
}
