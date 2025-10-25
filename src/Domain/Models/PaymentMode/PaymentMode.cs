namespace Domain.Models;

public class PaymentMode : Base
{
    public string BankAccounts { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
