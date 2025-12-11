namespace Library;

public class Manager : Staff
{
    private List<Report> _reports = new();
    public IReadOnlyList<Report> Reports => _reports.AsReadOnly();

    public Manager(string name, DateTime employmentDate, double baseSalary)
        : base(name, employmentDate, baseSalary)
    {
        Staff.AddStaffTToExtent(this);
    }

    public Report GenerateReport(ReportType type, string? description = null)
    {
        var report = new Report(this, type, description);

        _reports.Add(report);
        return report;
    }

    public void ManageShift(Shift shift)
    {
        
    }

    public void ManageRefunds(Refund refund)
    {
        
    }

    public void RemoveReport(Report r)
    {
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
}