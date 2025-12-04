using System;
using System.Collections.Generic;

[Serializable]
public class Supplier
{
    private static List<Supplier> _extent = new();
    public static List<Supplier> Extent => _extent;

    private string _name;

    // Qualified association: Model → Product
    private Dictionary<string, Product> _productsByModel = new();

    public string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_name))
                throw new Exception("Supplier name is empty");
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty");
            _name = value;
        }
    }

    public Supplier(string name)
    {
        Name = name;
        _extent.Add(this);
    }

    // QUALIFIED ASSOCIATION METHOD
    public void AddProduct(Product p)
    {
        if (p == null)
            throw new ArgumentNullException("Product cannot be null");

        string qualifier = p.Model;  // QUALIFIER = MODEL

        if (_productsByModel.ContainsKey(qualifier))
            throw new InvalidOperationException($"Product with model {qualifier} already exists for this supplier");

        _productsByModel[qualifier] = p;

        // Product’ın supplier referansı ayarlanır
        p.SetSupplier(this);
    }

    public Product GetProductByModel(string model)
    {
        if (_productsByModel.TryGetValue(model, out var product))
            return product;

        throw new KeyNotFoundException($"No product found with model {model}");
    }

    public IReadOnlyDictionary<string, Product> GetAllProducts()
    {
        return _productsByModel;
    }
}
