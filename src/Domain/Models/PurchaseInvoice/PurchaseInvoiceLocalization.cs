namespace Domain.Models;

public class PurchaseInvoiceLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int PurchaseInvoiceId { get; set; }
    public PurchaseInvoice PurchaseInvoice { get; set; }
}