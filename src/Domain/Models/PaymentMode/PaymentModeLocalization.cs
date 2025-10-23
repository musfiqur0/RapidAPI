namespace Domain.Models;

public class PaymentModeLocalization : Base
{
    public int PaymentModeId { get; set; }
    public PaymentMode PaymentMode { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}
