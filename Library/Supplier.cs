using System.Text.Json;

namespace Library;

[Serializable]
public class Supplier
{
 
    private static List<Supplier> _extent = new();
    public static IReadOnlyCollection<Supplier> Extent => _extent.AsReadOnly();

    
    private static Dictionary<string, Supplier> _suppliersByCompany = new();
    public static IReadOnlyDictionary<string, Supplier> SuppliersByCompany =>
        new Dictionary<string, Supplier>(_suppliersByCompany);


    public static void SaveExtent(string fileName = "supplier_extent.json")
    {
        var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "supplier_extent.json")
    {
        if (!File.Exists(fileName)) return;

        var json = File.ReadAllText(fileName);
        _extent = JsonSerializer.Deserialize<List<Supplier>>(json);

        _suppliersByCompany = _extent.ToDictionary(s => s.CompanyName, s => s);
    }



    private string _name;
    private string _companyName;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Supplier name cannot be empty.");
            _name = value;
        }
    }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Company name cannot be empty.");
            _companyName = value;
        }
    }


 
    private Dictionary<string, Product> _productsByModel = new();

    public IReadOnlyDictionary<string, Product> ProductsByModel =>
        new Dictionary<string, Product>(_productsByModel);


  
    public Supplier(string name, string companyName)
    {
        Name = name;
        CompanyName = companyName;

        if (_suppliersByCompany.ContainsKey(companyName))
            throw new InvalidOperationException(
                $"A supplier with company name '{companyName}' already exists."
            );

        _suppliersByCompany[companyName] = this;
        _extent.Add(this);
    }

    public Supplier() { }


  
    public void AddProduct(Product p)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p), "Product cannot be null.");

        string qualifier = p.Model;

        if (string.IsNullOrWhiteSpace(qualifier))
            throw new ArgumentException("Product model cannot be empty for qualified association.");

        if (_productsByModel.ContainsKey(qualifier))
            throw new InvalidOperationException(
                $"A product with model '{qualifier}' already exists for this supplier."
            );

        _productsByModel[qualifier] = p;

        if (p.Supplier != this)
            p.SetSupplier(this);
    }

    public Product GetProductByModel(string model)
    {
        if (_productsByModel.TryGetValue(model, out var product))
            return product;

        throw new KeyNotFoundException($"No product found for model '{model}'.");
    }

    public bool HasProduct(string model)
    {
        return _productsByModel.ContainsKey(model);
    }

    public void RemoveProduct(Product p)
    {
        if (p == null)
            throw new ArgumentNullException(nameof(p));

        if (!_productsByModel.ContainsKey(p.Model))
            throw new InvalidOperationException("This product is not registered under this supplier.");

        _productsByModel.Remove(p.Model);

        if (p.Supplier == this)
            p.RemoveSupplier();
    }


    
    public void Destroy()
    {
        _suppliersByCompany.Remove(this.CompanyName);

        foreach (var product in _productsByModel.Values.ToList())
            RemoveProduct(product);

        _extent.Remove(this);
    }



    public static Supplier GetSupplierByCompanyName(string companyName)
    {
        if (_suppliersByCompany.TryGetValue(companyName, out var supplier))
            return supplier;

        throw new KeyNotFoundException(
            $"No supplier found with company name '{companyName}'."
        );
    }
}
