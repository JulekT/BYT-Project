namespace Library;

public class PartTimeEmployment : EmploymentType
{
    public PartTimeEmployment(double hourlyRate = 18)
        : base("PartTime", hourlyRate)
    {
    }
}