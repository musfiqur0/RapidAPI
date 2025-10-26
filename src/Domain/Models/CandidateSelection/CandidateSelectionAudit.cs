namespace Domain.Models;

public class CandidateSelectionAudit : Audit
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string Position { get; set; }
    public string Team { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}