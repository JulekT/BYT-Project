using System;


[Serializable]
public class Product
{
    private static List<Product> _extent = new List<Product>();

    private string _name;
    private string _brand;
    private string _model;
    private double _price;
    private double _cost;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty");
            _name = value;
        }
    }

    public string Brand
    {
        get => _brand;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Brand cannot be empty");
            _brand = value;
        }
    }

    public string Model
    {
        get => _model;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Model cannot be empty");
            _model = value;
        }
    }

    public double Price
    {
        get => _price;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Price must be positive");
            _price = value;
        }
    }

    public double Cost
    {
        get => _cost;
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

        AddProduct(this);
    }

    private static void AddProduct(Product p)
    {
        if (p == null) throw new ArgumentException("Product cannot be null");
        _extent.Add(p);
    }

    public static List<Product> GetExtent() => new List<Product>(_extent);

    public static void SetExtent(List<Product> list) => _extent = list ?? new List<Product>();
}
