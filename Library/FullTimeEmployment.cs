namespace Library;

public class FullTimeEmployment : EmploymentType
{
    public FullTimeEmployment(double hourlyRate = 30) 
        : base("FullTime", hourlyRate)
    {
    }
}