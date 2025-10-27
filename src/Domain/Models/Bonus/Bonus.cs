namespace Domain.Models;

public class Bonus
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public long IqamaNo { get; set; }
    public int BranchId { get; set; }
    public string BonusTypeId { get; set; }
    public decimal BonusAmount { get; set; }
    public string Note { get; set; }
}
