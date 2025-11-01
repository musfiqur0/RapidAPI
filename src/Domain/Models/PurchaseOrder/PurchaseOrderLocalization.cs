namespace Domain.Models;

public class PurchaseOrderLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }
}