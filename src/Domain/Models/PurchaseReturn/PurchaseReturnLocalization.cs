namespace Domain.Models;

public class PurchaseReturnLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int PurchaseReturnId { get; set; }
    public PurchaseReturn PurchaseReturn { get; set; }
}
