using System.Text.Json;

namespace Library;


public enum ReportType
{
    Sales,
    Employee,
    Inventory
}

[Serializable]
public class Report
{
    private static List<Report> _extent = new();
    public static IReadOnlyList<Report> Extent => _extent.AsReadOnly();

    public Manager Manager { get; private set; }
    public int ReportID { get; private set; }
    public ReportType Type { get; private set; }
    public DateTime GeneratedDate { get; private set; }
    public string? Description { get; private set; }

    private static int _lastID = 0;

    public Report() { } // Needed for JSON deserialization

    public Report(Manager manager, ReportType type, string? description = null)
    {
        if (manager == null)
            throw new ArgumentException("Report must be associated with a Manager.");

        Manager = manager;
        Type = type;
        Description = description;
        GeneratedDate = DateTime.Now;

        ReportID = ++_lastID;

        _extent.Add(this);
    }

    public void Destroy()
    {
        _extent.Remove(this);
        Manager = null;
    }

    public static void SaveExtent(string fileName = "report_extent.json")
    {
        var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(fileName, json);
    }

    public static void LoadExtent(string fileName = "report_extent.json")
    {
        if (!File.Exists(fileName)) return;

        var json = File.ReadAllText(fileName);
        var list = JsonSerializer.Deserialize<List<Report>>(json);
        if (list != null)
        {
            _extent = list;
            if (_extent.Count > 0)
                _lastID = _extent.Max(r => r.ReportID);
        }
    }

    public override string ToString()
    {
        return $"Report #{ReportID} | Type: {Type} | Manager: {Manager?.Name} | Date: {GeneratedDate}";
    }
}