using Library;

[Serializable]
public class Product
{
    private static List<Product> _extent = new();
    public static List<Product> Extent
    {
        get => _extent;
        set
        {
            if (value == null)
                throw new ArgumentException("Product Extent is null");
            _extent = value;
        }
    }

    private string _name;
    private string _brand;
    private string _model;
    private double _price;
    private double _cost;

    public string Name
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_name))
                throw new ValueNotAssigned("Product name is empty, you need to assign it first");
            else
                return _name;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty");
            _name = value;
        }
    }

    public string Brand
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_brand))
                throw new ValueNotAssigned("Product brand is empty, you need to assign it first");
            else
                return _brand;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Brand cannot be empty");
            _brand = value;
        }
    }

    public string Model
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_model))
                throw new ValueNotAssigned("Product model is empty, you need to assign it first");
            else
                return _model;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Model cannot be empty");
            _model = value;
        }
    }

    public double Price
    {
        get
        {
            if (_price <= 0)
                throw new NumberIsNotPositive("Price must be positive, you need to assign it first");
            else
                return _price;
        }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Price must be positivea");
            _price = value;
        }
    }

    public double Cost
    {
        get
        {
            if (_cost <= 0)
                throw new NumberIsNotPositive("Product cost must be positive, you need to assign it first");
            else
                return _cost;
        }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Cost must be positive");
            _cost = value;
        }
    }

    public Product() { }

    public Product(string name, string brand, string model, double price, double cost)
    {
        Name = name;
        Brand = brand;
        Model = model;
        Price = price;
        Cost = cost;

        AddProductToExtent(this);
    }

    public static void AddProductToExtent(Product p)
    {
        if (p == null) 
            throw new ArgumentException("Product cannot be null");
        Extent.Add(p);
    }

    public static void SetExtent(List<Product> list) => Extent = list ?? new List<Product>();

    public static void SaveExtent(string fileName = "product_extent.json")
    {
        var json = JsonSerializer.Serialize(Extent, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "product_extent.json")
    {
        if (!File.Exists(fileName)) return;

        var json = File.ReadAllText(fileName);
        Extent = JsonSerializer.Deserialize<List<Product>>(json);
    }

    
}
