namespace Library;

public class EmploymentType
{
    public EmploymentTypeEnum Type { get; private set; }
    public double HourlyRate { get; private set; }

    public EmploymentType(EmploymentTypeEnum type, double hourlyRate)
    {
        if (hourlyRate <= 0)
            throw new ArgumentException("Hourly rate must be positive.");

        Type = type;
        HourlyRate = hourlyRate;
    }

    public override string ToString()
        => $"{Type} ({HourlyRate}/hour)";
}