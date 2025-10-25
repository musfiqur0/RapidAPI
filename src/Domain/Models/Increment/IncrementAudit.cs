namespace Domain.Models;

public class IncrementAudit : Audit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public int EmployeeId { get; set; }
    public string Branch { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
