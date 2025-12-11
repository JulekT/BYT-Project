using Library;
using System.Text.Json;

namespace BYT_Project
{
    public abstract class Staff
    {
        private static List<Staff> _extent = new();
        public static List<Staff> Extent
        {
            get => _extent;
            set
            {
                if (value == null)
                    throw new ArgumentException("Staff Extent is null");
                _extent = value;
            }
        }

        public static double YearlySalaryGrowthPercentage = 0.05;

        private string _name;
        private DateTime _employmentDate;
        private double _baseSalary;


        public string Name
        {
            get 
            {
                if (String.IsNullOrEmpty(_name))
                    throw new ValueNotAssigned("Staff name is null, you need to assign it firstly");
                return _name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name can't be empty");
                _name = value;
            }
        }

        public DateTime EmploymentDate
        {
            get 
            {
                if (_employmentDate == DateTime.MinValue)
                    throw new ValueNotAssigned("Staff employement date is null, you need to assign it firstly");
                return _employmentDate;
            }
            set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentNullException("Employment Date can't be empty");
                _employmentDate = value;
            }
        }

        public double Salary
        {
            get
            {
                if (_baseSalary < 0)
                    throw new NumberIsNotPositive("Staff salary is not positive, that's illigal dude");

                int yearsSinceEmployment = EmploymentDate.Year - _employmentDate.Year;
                return _baseSalary * (1 + YearlySalaryGrowthPercentage * yearsSinceEmployment);
            }
        
        }

        public Staff() { }

        public Staff(string name, DateTime employmentDate, double baseSalary)
        {
            _name = name;
            _employmentDate = employmentDate;
            _baseSalary = baseSalary;
        }

        public Staff Manager;
        private HashSet<Staff> _managedStaff = new();
        public IReadOnlyCollection<Staff> ManagedStaff => _managedStaff.ToList().AsReadOnly();

        public void AddManager(Staff manager)
        {
            this.Manager = manager;
            if(!manager.ManagedStaff.Contains(this))
                manager.AddManagedStaff(this);
        }

        public void RemoveManager()
        {
            if(this.Manager.ManagedStaff.Contains(this))
                this.Manager.RemoveManagedStaff(this);
            this.Manager = null;

        }

        public void AddManagedStaff(Staff staff)
        {
            if(staff == null)
                throw new ArgumentNullException(nameof(staff));
            if (_managedStaff.Contains(staff))
                return;

            _managedStaff.Add(staff);

            if (staff.Manager != this)
                staff.AddManager(this);
        }

        public void RemoveManagedStaff(Staff staff)
        {
            if(staff == null)
                throw new ArgumentNullException(nameof(staff));
            if(this._managedStaff.Contains(staff))
                _managedStaff.Remove(staff);
            if (staff.Manager == this)
                staff.RemoveManager();
        }

        public static void AddStaffTToExtent(Staff s)
        {
            if (s == null)
                throw new ArgumentException("Staff cannot be null");
            Extent.Add(s);
        }

        public static void SaveExtent(string fileName = "staff_extent.json")
        {
            var json = JsonSerializer.Serialize(
                Extent,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(fileName, json);
        }

        public static void LoadExtent(string fileName = "staff_extent.json") {
            if (!File.Exists(fileName))
                return;

            var json = File.ReadAllText(fileName);
            Extent = JsonSerializer.Deserialize<List<Staff>>(json);
        }
    }
}