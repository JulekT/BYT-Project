using System.Text.Json;

namespace Library;

[Serializable]
public class Order
{
    private static int _lastID = 0;
    private static List<Order> _extent = new();

    public static List<Order> Extent
    {
        get => _extent;
        set => _extent = value ?? new List<Order>();
    }

    public int OrderID { get; private set; }
    public DateTime OrderDate { get; private set; }

    public double FinalPrice => CalculateFinalPrice();

    private List<ProductQuantityInOrder> _orderItems = new();

    public Refund Refund { get; private set; }



    public SalesPerson SalesPerson { get; private set; }

    public Order(SalesPerson salesPerson)
    {
        if (salesPerson == null)
            throw new ArgumentException("Order must be associated with a SalesPerson.");

        SalesPerson = salesPerson;

        OrderID = ++_lastID;
        OrderDate = DateTime.Now;

        Extent.Add(this);
    }


    public void AddProduct(Product product, int quantity)
    {
        var pq = new ProductQuantityInOrder(product, quantity);
        _orderItems.Add(pq);
    }

    private double CalculateFinalPrice()
    {
        double sum = 0;

        foreach (var item in _orderItems)
            sum += item.GetTotalPrice();

        return sum;
    }

    public IReadOnlyList<ProductQuantityInOrder> GetItems()
    {
        return _orderItems.AsReadOnly();
    }

    public Refund CreateRefund(string issue)
    {
        if (Refund != null)
            throw new InvalidOperationException("This order already has a refund.");

        Refund = new Refund(this, issue);
        return Refund;
    }


    public static void SaveExtent(string fileName = "order_extent.json")
    {
        var json = JsonSerializer.Serialize(Extent, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "order_extent.json")
    {
        if (!File.Exists(fileName)) return;

        var json = File.ReadAllText(fileName);
        Extent = JsonSerializer.Deserialize<List<Order>>(json);
    }
}