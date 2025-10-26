namespace Domain.Models;

public class TerminationAudit : Audit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public long IqamaNo { get; set; }
    public int BranchId { get; set; }
    public string TerminationType { get; set; }
    public string Description { get; set; }
    public short StatusId { get; set; }
}