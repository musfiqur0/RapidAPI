namespace Domain.Models;

public class Deduction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public long IqamaNo { get; set; }
    public int BranchId { get; set; }
    public int DeductionTypeId { get; set; }
    public decimal DeductionAmount { get; set; }
    public string Notes { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
