using System.Text.Json;

namespace Library;

[Serializable]
public class Product
{
     
    private static List<Product> _extent = new();
    public static IReadOnlyCollection<Product> Extent => _extent.AsReadOnly();

    public static void SaveExtent(string fileName = "product_extent.json")
    {
        var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "product_extent.json")
    {
        if (!File.Exists(fileName)) return;

        var json = File.ReadAllText(fileName);
        _extent = JsonSerializer.Deserialize<List<Product>>(json);
    }


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
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty");
            _name = value;
        }
    }

    public string Brand
    {
        get => _brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Brand cannot be empty");
            _brand = value;
        }
    }

    public string Model
    {
        get => _model;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
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


  

    public Supplier Supplier { get; private set; }

    public void SetSupplier(Supplier supplier)
    {
        
        if (supplier == null)
            throw new ArgumentNullException(nameof(supplier));

        
        Supplier = supplier;
        
        
    }

    public void RemoveSupplier()
    {
        Supplier = null;
    }

    public Aisle Aisle { get; private set; }

    public void SetAisle(Aisle aisle)
    {
        if(aisle == null)
            throw new ArgumentNullException(nameof (aisle));

        this.Aisle = aisle;
        if(!this.Aisle.Products.Contains(this))
            this.Aisle.AddProduct(this);
    }

    public void RemoveAisle()
    {
        if (Aisle.Products.Contains(this))
            this.Aisle.RemoveProduct(this);
        this.Aisle = null;
    }


    public Product(string name, string brand, string model, double price, double cost)
    {
        Name = name;
        Brand = brand;
        Model = model;
        Price = price;
        Cost = cost;

        _extent.Add(this);
    }

    public Product() { }
}