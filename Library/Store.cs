using System.Text.Json;

namespace Library;

[Serializable]
public class Store
{
    private static List<Store> _extent = new();
    public static IReadOnlyCollection<Store> Extent => _extent.AsReadOnly();

    private string _name;
    private string _street;
    private string _city;
    private string _postalCode;
    private string _country;

    public string Name
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_name))
                throw new ValueNotAssigned("Store name is empty, you need to assign it first");
            return _name;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Store name cannot be empty");
            _name = value;
        }
    }

    public string Street
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_street))
                throw new ValueNotAssigned("Store street is empty, you need to assign it first");
            return _street;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Street cannot be empty");
            _street = value;
        }
    }

    public string City
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_city))
                throw new ValueNotAssigned("Store city is empty, you need to assign it first");
            return _city;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("City cannot be empty");
            _city = value;
        }
    }

    public string PostalCode
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_postalCode))
                throw new ValueNotAssigned("Store postal code is empty, you need to assign it first");
            return _postalCode;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Postal code cannot be empty");
            _postalCode = value;
        }
    }

    public string Country
    {
        get
        {
            if (String.IsNullOrWhiteSpace(_country))
                throw new ValueNotAssigned("Store country is empty, you need to assign it first");
            return _country;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Country cannot be empty");
            _country = value;
        }
    }


    private HashSet<Aisle> _aisles = new();
    public IReadOnlyCollection<Aisle> Aisles => _aisles.ToList().AsReadOnly();

    public void AddAisle(Aisle aisle)
    {
        if (aisle == null)
            throw new ArgumentNullException(nameof(aisle));

        if (_aisles.Contains(aisle))
            return;

        if (aisle.Store != null && aisle.Store != this)
            throw new InvalidOperationException("This aisle already belongs to another store.");

        _aisles.Add(aisle);

        if (aisle.Store != this)
            aisle.SetStore(this);
    }

    public void RemoveAisle(Aisle aisle)
    {
        if (aisle == null)
            throw new ArgumentNullException(nameof(aisle));

        if (!_aisles.Contains(aisle))
            throw new InvalidOperationException("This aisle does not belong to this store.");

        if (_aisles.Count == 1)
            throw new InvalidOperationException("A store must have at least one aisle (1..*). Cannot remove the last one.");

        _aisles.Remove(aisle);

        aisle.Destroy();

    }

    private HashSet<Shift> _shifts = new();
    public IReadOnlyCollection<Shift> Shifts => _shifts.ToList().AsReadOnly();

    public void AddShift(Shift shift)
    {
        if (shift == null)
            throw new ArgumentNullException(nameof(shift));
        if (_shifts.Contains(shift))
            return;
        _shifts.Add(shift);
        if(shift.Store != this)
            shift.AssignStore(this);
    }
    public void RemoveShift(Shift shift)
    {
        if (shift == null)
            throw new ArgumentNullException(nameof(shift));
        if (_shifts.Contains(shift))
            _shifts.Remove(shift);
        if (shift.Store == this)
            shift.RemoveStore();
    }
    public Store() { }

    public Store(string name, string street, string city, string postalCode, string country)
    {
        Name = name;
        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;

        _extent.Add(this);
    }


    public void Destroy()
    {
        foreach (var aisle in _aisles.ToList())
            aisle.Destroy();

        _aisles.Clear();
        _extent.Remove(this);
    }


    public static void SaveExtent(string fileName = "store_extent.json")
    {
        var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "store_extent.json")
    {
        if (!File.Exists(fileName))
            return;

        var json = File.ReadAllText(fileName);
        _extent = JsonSerializer.Deserialize<List<Store>>(json);
    }
    private List<Stock> _stock = new();
    public IReadOnlyList<Stock> Stock => _stock.AsReadOnly();

    public void AddStock(Stock s)
    {
        if (!_stock.Contains(s))
            _stock.Add(s);
    }

    public void RemoveStock(Stock s)
    {
        _stock.Remove(s);
    }

    public Stock? GetStockForProduct(Product p)
    {
        return _stock.FirstOrDefault(s => s.Product == p);
    }


}