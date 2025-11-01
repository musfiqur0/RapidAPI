namespace Application.DTOs;

public class PurchaseOrderAddEditDto
{
    public int Id { get; set; } = 0;
    public int DocumentNumber { get; set; }
    public string PONumber { get; set; }
    public DateTime PODate { get; set; }
    public int SupplierId { get; set; }
    public int PaymentModeId { get; set; }
    public int DueDays { get; set; }
    public DateTime PaymentDate { get; set; }
    public string SupplierNumber { get; set; }
    public int SupplierStatusId { get; set; }
    public int SupplierGroupId { get; set; }
    public string Remarks { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public int UnitId { get; set; }
    public decimal Rate { get; set; }
}