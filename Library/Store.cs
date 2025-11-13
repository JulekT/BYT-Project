using Library;
using System.Text.Json;

[Serializable]
public class Store
{
    private static List<Store> _extent = new();
    public static List<Store> Extent
    {
        get => _extent;
        set
        {
            if (value == null)
                throw new ArgumentException("Store Extent is null");
            _extent = value;
        }
    }

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

    public Store() { }

    public Store(string name, string street, string city, string postalCode, string country)
    {
        Name = name;
        Street = street;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }

    public static void AddStoreToExtent(Store s)
    {
        if (s == null) 
            throw new ArgumentException("Store cannot be null");
        _extent.Add(s);
    }

    public static void SaveExtent(string fileName = "store_extent.json")
    {
        var json = JsonSerializer.Serialize(
            _extent,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "store_extent.json")
    {
        if (!File.Exists(fileName))
            return;

        var json = File.ReadAllText(fileName);
        _extent = JsonSerializer.Deserialize<List<Store>>(json);
    }
}
