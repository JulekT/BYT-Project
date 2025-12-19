using System.Text.Json;

namespace Library
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

        public Store? Store { get; private set; }  

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
                    throw new NumberIsNotPositive("Staff salary is not positive");

                int yearsSinceEmployment =
                    (int)((DateTime.Now - EmploymentDate).TotalDays / 365);

                if (yearsSinceEmployment < 0)
                    yearsSinceEmployment = 0;

                return _baseSalary *
                       (1 + YearlySalaryGrowthPercentage * yearsSinceEmployment);
            }
        }

        public bool IsManager {get; private set;}
        public bool IsSalesPerson {get; private set;}
        public bool IsCashier {get; private set;}
        
        public Staff(string name, DateTime employmentDate, double baseSalary,
            bool isSalesPerson = false, bool isCashier = false, bool isManager = false,
            double? commissionRate = null, EmploymentType? employmentType = null)
        {
            _name = name;
            _employmentDate = employmentDate;
            _baseSalary = baseSalary;
            IsSalesPerson = isSalesPerson;
            IsCashier = isCashier;
            IsManager = isManager;
            
            if (IsSalesPerson)
            {
                _commissionRate = commissionRate;
                _orders = new();
            }

            if (IsManager)
            {
                _reports = new();
            }

            if (IsCashier)
            {
                _employmentType = employmentType;
            }
        }
        
        // Cashier
        private EmploymentType? _employmentType;

        public EmploymentType? EmploymentType
        {
            get => _employmentType;
            private set
            {
                if (value == null && IsCashier)
                    throw new ValueNotAssigned("Staff is a cashier, employmentType needs to be assigned");
                _employmentType = value;
            }
        }
        public void ChangeEmploymentType(EmploymentType newType)
        {
            if (newType == null && IsCashier)
                throw new ArgumentNullException(nameof(newType));

            EmploymentType = newType;
        }
        
        // Manager
        private List<Report>? _reports;
        public IReadOnlyList<Report>? Reports => _reports.AsReadOnly();
        public Report GenerateReport(ReportType type, string? description = null)
        {
            if(!IsManager)
                throw new ArgumentException("Staff is not a manager, can't generate report.");
            var report = new Report(this, type, description);

            _reports.Add(report);
            return report;
        }
        public void ManageShift(Shift shift)
        {
            if(!IsManager)
                throw new ArgumentException("Staff is not a manager, can't manage shift.");
        }

        public void ManageRefunds(Refund refund)
        {
            if(!IsManager)
                throw new ArgumentException("Staff is not a manager, can't manage refunds.");
        }

        public void RemoveReport(Report r)
        {
            if(!IsManager)
                throw new ArgumentException("Staff is not a manager, can't manage reports.");
            if (r == null)
                throw new ArgumentNullException(nameof(r));

            if (!_reports.Contains(r))
                throw new InvalidOperationException("This report is not associated with this manager.");

            _reports.Remove(r);

            r.Destroy();
        }
        
        public void Destroy()
        {
            foreach (var r in _reports.ToList())
            {
                r.Destroy();
            }

            _reports.Clear();
        }
        
        // SalesPerson
        private double? _commissionRate;
        private List<Order>? _orders;
        public double? CommissionRate
        {
            get => _commissionRate;
            set
            {
                if(value == null && !IsSalesPerson)
                    throw new ValueNotAssigned("Staff is a salesperson, commission cannot has to be assigned.");
                if (value < 0 || value > 1)
                    throw new ArgumentException("Commission rate must be between 0 and 1.");
                _commissionRate = value;
            }
        }
        public Order RegisterOrder(List<(Product product, int quantity)> items)
        {
            if (!IsSalesPerson)
                throw new ArgumentException("Staff is not a salesperson");
            if (items == null || items.Count == 0)
                throw new ArgumentException("Order must contain at least one item.");

            Order order = new Order(this);

            foreach (var item in items)
                order.AddProduct(item.product, item.quantity);

            _orders.Add(order);
            return order;
        }
        // Subclass Changing methods
        public void BecomeManager()
        {
            IsManager = true;
            _reports = new();
        }

        public void StopBeingManager()
        {
            IsManager = false;
            foreach (var r in _reports)
            {
                r.Destroy();
            }
            _reports.Clear();
            _reports = null;
        }

        public void BecomeSalesPerson(double commisionRate = 0)
        {
            IsSalesPerson = true;
            _commissionRate = commissionRate;
            _orders = new();
        }

        public void StopBeingSalesPerson()
        {
            IsSalesPerson = false;
            _commissionRate = null;
        }

        public void BecomeCashier(EmploymentType employmentType)
        {
            IsCashier = true;
            _employmentType = employmentType;
        }

        public void StopBeingCashier()
        {
            IsCashier = false;
            _employmentType = null;
        }
        
        // -------------------------------------------------------------
        public void AssignStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException(nameof(store));

            if (Store != null && Store != store)
                throw new InvalidOperationException("Staff cannot be assigned to another Store.");

            Store = store;
        }


        public Staff? Manager;
        private HashSet<Staff> _managedStaff = new();
        public IReadOnlyCollection<Staff> ManagedStaff => _managedStaff.ToList().AsReadOnly();

        public void AddManager(Staff manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));
            this.Manager = manager;
            if(!manager.ManagedStaff.Contains(this))
                manager.AddManagedStaff(this);
        }

        public void RemoveManager()
        {
            if (Manager.ManagedStaff.Contains(this))
                Manager.RemoveManagedStaff(this);
            Manager = null;
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

        private HashSet<Shift> _shifts = new();
        public IReadOnlyCollection<Shift> Shifts => _shifts.ToList().AsReadOnly();

        public void AssignToShift(Shift shift)
        {
            if (shift == null)
                throw new ArgumentNullException(nameof(shift));

            if (Store == null)
                throw new InvalidOperationException("Staff must belong to a Store before being assigned to a Shift.");

            if (shift.Store != Store)
                throw new InvalidOperationException("Staff cannot work in a Shift belonging to another Store.");

            if (!_shifts.Contains(shift))
                _shifts.Add(shift);

            if (!shift.Staff.Contains(this))
                shift.AddStaff(this);
        }

        public void RemoveFromShift(Shift shift)
        {
            if (shift == null)
                throw new ArgumentNullException(nameof(shift));

            _shifts.Remove(shift);

            if (shift.Staff.Contains(this))
                shift.RemoveStaff(this);
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
