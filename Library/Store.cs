using System.Text.Json;

namespace Library;

[Serializable]
public class Store
{
    private static List<Store> _extent = new();
    public static IReadOnlyCollection<Store> Extent => _extent.AsReadOnly();

    private string _name;
    private Address _address;

    public string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_name))
                throw new ValueNotAssigned("Store name is empty, you need to assign it first");
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Store name cannot be empty");
            _name = value;
        }
    }

    public Address Address
    {
        get
        {
            if (_address == null)
                throw new ValueNotAssigned("Store address is not assigned");
            return _address;
        }
        private set
        {
            _address = value ?? throw new ArgumentNullException(nameof(Address));
        }
    }

    /* =======================
       Associations
       ======================= */

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
            throw new InvalidOperationException(
                "A store must have at least one aisle (1..*). Cannot remove the last one."
            );

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

        if (shift.Store != this)
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

    private List<Stock> _stock = new();
    public IReadOnlyList<Stock> Stock => _stock.AsReadOnly();

    public void AddStock(Stock s)
    {
        if (s == null)
            throw new ArgumentNullException(nameof(s));

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

    /* =======================
       Constructors
       ======================= */

    // New constructor using complex attribute
    public Store(string name, Address address)
    {
        Name = name;
        Address = address;
        _extent.Add(this);
    }

    // Backward-compatible constructor (flattened input)
    public Store(
        string name,
        string street,
        string city,
        string postalCode,
        string country
    ) : this(name, new Address(street, city, postalCode, country))
    {
    }

    public Store() { }

    /* =======================
       Lifecycle & Persistence
       ======================= */

    public void Destroy()
    {
        foreach (var aisle in _aisles.ToList())
            aisle.Destroy();

        _aisles.Clear();
        _extent.Remove(this);
    }

    public static void SaveExtent(string fileName = "store_extent.json")
    {
        var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "store_extent.json")
    {
        if (!File.Exists(fileName))
            return;

        var json = File.ReadAllText(fileName);
        _extent = JsonSerializer.Deserialize<List<Store>>(json) ?? new();
    }
}
