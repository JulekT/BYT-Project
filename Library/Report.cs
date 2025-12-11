using System.Text.Json;

namespace Library
{
    // Enum for report types (Sales, Employee, Inventory)
    public enum ReportType
    {
        Sales,
        Employee,
        Inventory
    }

    public class Report
    {
        // Static Auto-Increment ID
        private static int _lastID = 0;
        
        // Fields
        private ReportType _type;
        private string? _description;

        // Properties (with Validation)
        public int ReportID { get; private set; }

        public ReportType Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        public DateTime GeneratedDate { get; private set; }

        public string? Description
        {
            get => _description;
            set
            {
                if (value != null && value.Trim().Length == 0)
                    throw new ArgumentException("Description cannot be empty string. Use null instead.");

                _description = value;
            }
        }

        
        // Constructors
        
        public Report() { } // Required for JSON deserialization

        public Report(ReportType type, string? description = null)
        {
            ReportID = ++_lastID;
            Type = type;
            GeneratedDate = DateTime.Now;
            Description = description;
        }

        // Extent (Static Storage)
        private static List<Report> _extent = new();
        public static IReadOnlyList<Report> Extent => _extent;

        public static void AddToExtent(Report r)
        {
            if (r == null)
                throw new ArgumentException("Report cannot be null.");

            _extent.Add(r);
        }
        
        // JSON Persistence
        
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
            if (!File.Exists(fileName))
                return;

            var json = File.ReadAllText(fileName);
            var list = JsonSerializer.Deserialize<List<Report>>(json);

            if (list != null)
                _extent = list;
            
            if (_extent.Count > 0)
                _lastID = _extent.Max(r => r.ReportID);
        }
        
        // ToString Override
        public override string ToString()
        {
            return $"Report #{ReportID} | Type: {Type} | Date: {GeneratedDate} | Description: {Description}";
        }
    }
}
