namespace Library;

public abstract class EmploymentType
{
    private string _name;
    private double _hourlyRate;

    public string Name
    {
        get => _name;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Employment type name cannot be empty.");
            _name = value;
        }
    }

    public double HourlyRate
    {
        get => _hourlyRate;
        protected set
        {
            if (value <= 0)
                throw new ArgumentException("Hourly rate must be positive.");
            _hourlyRate = value;
        }
    }

    protected EmploymentType(string name, double hourlyRate)
    {
        Name = name;
        HourlyRate = hourlyRate;
    }

    public override string ToString() => $"{Name} ({HourlyRate}/hour)";
}