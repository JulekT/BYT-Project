using Library;

[Serializable]
public class Aisle
{
    private static List<Aisle> _extent = new();
    public static List<Aisle> Extent
    {
        get => _extent;
        set
        {
            if (value == null)
                throw new ArgumentException("Aisle Extent is null");
            _extent = value;
        }
    }

    private string _name;

    public string Name
    {
        get
        {
            if (String.IsNullOrEmpty(_name))
                throw new ValueNotAssigned("Aisle name is empty, you need to assign it firstly");
            else
                return _name;
        }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Aisle name cannot be empty");
            _name = value;
        }
    }
    
    public Store Store { get; private set; }
    
    internal void SetStore(Store s)
    {
        if (s == null)
            throw new ArgumentException("Store cannot be null.");

        Store = s;
    }
    
    internal void RemoveStore()
    {
        Store = null;
    }

    public Aisle() { }

    public Aisle(string name) => Name = name;

    public static void AddAisleToExtent(Aisle a)
    {
        if (a == null) 
            throw new ArgumentException("Aisle cannot be null");
        _extent.Add(a);
    }

    public static List<Aisle> GetExtent() => new List<Aisle>(_extent);

    public static void SetExtent(List<Aisle> list) => _extent = list ?? new List<Aisle>();

    public static void SaveExtent(string fileName = "aisle_extent.json")
    {
        var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "aisle_extent.json")
    {
        if (!File.Exists(fileName)) return;

        var json = File.ReadAllText(fileName);
        _extent = JsonSerializer.Deserialize<List<Aisle>>(json);
    }
}
