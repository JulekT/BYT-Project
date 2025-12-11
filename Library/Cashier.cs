namespace Library;

public class Cashier : Staff
{
    private EmploymentType _employmentType;

    public EmploymentType EmploymentType
    {
        get => _employmentType;
        private set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(EmploymentType), 
                    "Cashier must have an employment type.");
            _employmentType = value;
        }
    }

    public Cashier(string name, DateTime employmentDate, double baseSalary, EmploymentType employmentType)
        : base(name, employmentDate, baseSalary)
    {
        EmploymentType = employmentType;

        Staff.AddStaffTToExtent(this);
    }

    public void ChangeEmploymentType(EmploymentType newType)
    {
        if (newType == null)
            throw new ArgumentNullException(nameof(newType), 
                "Employment type cannot be null.");

        EmploymentType = newType;
    }

    public void ProcessOrder(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

    }
}