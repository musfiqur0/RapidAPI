namespace Domain.Models;

public class Allowance
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public long IqamaNo { get; set; }
    public int BranchId { get; set; }
    public int AllowanceTypeId { get; set; }
    public decimal AllowanceAmount { get; set; }
    public string Notes { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
