namespace Domain.Models;

public class BenefitPenaltyAudit:Audit
{
    public int Id { get; set; }
    public int TypeId { get; set; }
    public int SubjectId { get; set; }
    public string Criteria { get; set; }
    public DateTime Date { get; set; }
    public int DriverId { get; set; }
    public int FormalityId { get; set; }
    public string Description { get; set; }
}
