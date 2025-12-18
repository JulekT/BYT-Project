using System.Text.Json;

namespace Library
{
    public class Payment
    {
        private static List<Payment> _extent = new();
        public static IReadOnlyList<Payment> Extent => _extent.AsReadOnly();

        public int PaymentId { get; }
        public PaymentType Type { get; }
        public double Amount { get; }
        public DateTime Date { get; }

        public PaymentDetails Details { get; }

        public Payment(
            int paymentId,
            double amount,
            DateTime date,
            PaymentType type,
            PaymentDetails details)
        {
            if (amount < 0)
                throw new ArgumentException("Amount must be positive.");

            if (details == null)
                throw new ArgumentNullException(nameof(details));

            PaymentId = paymentId;
            Amount = amount;
            Date = date;
            Type = type;
            Details = details;

            _extent.Add(this);
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
            _extent = JsonSerializer.Deserialize<List<Payment>>(json) ?? new();
        }
    }
}