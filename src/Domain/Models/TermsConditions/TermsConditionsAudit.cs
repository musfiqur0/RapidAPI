namespace Domain.Models;

public class TermsConditionsAudit : Audit
{
    public int Id { get; set; }
    public int TermsConditionsId { get; set; }
    public string Name { get; set; }
}