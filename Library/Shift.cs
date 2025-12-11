using System.Text.Json;

namespace Library
{
    public class Shift
    {
       
        private DateTime _startTime;
        private DateTime _endTime;
        private DateTime _date;
        
    
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("Start time cannot be empty.");

                _startTime = value;
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("End time cannot be empty.");

                if (_startTime != DateTime.MinValue && value <= _startTime)
                    throw new ArgumentException("End time must be later than start time.");

                _endTime = value;
            }
        }

        public Store? Store;

        private HashSet<Staff> _staff = new();
        public IReadOnlyCollection<Staff> Staff => _staff.ToList().AsReadOnly();

        public void AssignStore(Store store)
        {
            Store = store;
            if (!Store.Shifts.Contains(this))
                Store.AddShift(this);
        }
        public void AddStaff(Staff staff)
        {
            if (staff == null)
                throw new ArgumentNullException(nameof(staff));

            if (_staff.Contains(staff))
                return;

            _staff.Add(staff);
            
            if (!staff.Shifts.Contains(this))
                staff.AssignToShift(this);
        }

        public void RemoveStore()
        {
            Store = null;
        }
        public void RemoveStaff(Staff staff)
        {
            if (staff == null)
                throw new ArgumentNullException(nameof(staff));

            if (_staff.Contains(staff))
                _staff.Remove(staff);

            if (staff.Shifts.Contains(this))
                staff.RemoveFromShift(this);
        }


        public DateTime Date
        {
            get => _date;
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("Date cannot be empty.");

                _date = value.Date; // Only the day matters
            }
        }

     
        public Shift() { }
        public Shift(DateTime date, DateTime startTime, DateTime endTime)
        {
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        
        public bool ConflictsWith(Shift other)
        {
            if (other == null)
                return false;

          
            if (this.Date.Date != other.Date.Date)
                return false;

            
            bool overlap = this.StartTime < other.EndTime &&
                           other.StartTime < this.EndTime;

            return overlap;
        }
        
        
        private static List<Shift> _extent = new();
        public static IReadOnlyList<Shift> Extent => _extent;

        public static void AddToExtent(Shift s)
        {
            if (s == null)
                throw new ArgumentException("Shift cannot be null.");

            _extent.Add(s);
        }
        
        public static void SaveExtent(string fileName = "shift_extent.json")
        {
            var json = JsonSerializer.Serialize(_extent, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(fileName, json);
        }

        public static void LoadExtent(string fileName = "shift_extent.json")
        {
            if (!File.Exists(fileName))
                return;

            var json = File.ReadAllText(fileName);
            var list = JsonSerializer.Deserialize<List<Shift>>(json);

            if (list != null)
                _extent = list;
        }
        
        
        public override string ToString()
        {
            return $"{Date.ToShortDateString()} | {StartTime:HH:mm} - {EndTime:HH:mm}";
        }
    }
}
