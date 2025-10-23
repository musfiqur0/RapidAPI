namespace Domain.Models;

public class PaymentModeAudit : Audit
{
    public int Id { get; set; }
    public int PaymentModeId { get; set; }
    public string Name { get; set; }
    public string BankAccounts { get; set; } = string.Empty;
    public bool Default { get; set; }
    public bool Draft { get; set; }
}