namespace Application.DTOs;

public class PaymentModeAddEditDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public string BankAccounts { get; set; } = string.Empty;
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
