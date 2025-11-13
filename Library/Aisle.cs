[Serializable]
public class Aisle
{
    private static List<Aisle> _extent = new List<Aisle>();

    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Aisle name cannot be empty");
            _name = value;
        }
    }

    public Aisle() { }

    public Aisle(string name)
    {
        Name = name;

        AddAisle(this);
    }

    private static void AddAisle(Aisle a)
    {
        if (a == null) throw new ArgumentException("Aisle cannot be null");
        _extent.Add(a);
    }

    public static List<Aisle> GetExtent() => new List<Aisle>(_extent);

    public static void SetExtent(List<Aisle> list) => _extent = list ?? new List<Aisle>();
}