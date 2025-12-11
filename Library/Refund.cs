namespace Library;

[Serializable]


public class Refund
{
    private static List<Refund> _extent = new();
    public static List<Refund> Extent => _extent;

    public int RefundID { get; private set; }
    private static int _lastRefundID = 0;

    public string IssueDescription { get; private set; }

    public Order Order { get; private set; } // mandatory 1

    public Refund(Order order, string issueDescription)
    {
        if (order == null)
            throw new ArgumentException("Refund must be linked to an Order.");

        RefundID = ++_lastRefundID;
        IssueDescription = issueDescription ?? "";

        Order = order;

        _extent.Add(this);
    }
}