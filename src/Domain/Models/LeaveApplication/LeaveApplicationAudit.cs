namespace Domain.Models;

public class LeaveApplicationAudit : Audit
{
    public int Id { get; set; }
    public int LeaveApplicationId { get; set; }
    public LeaveApplication LeaveApplication { get; set; }
    public int LeaveTypesId { get; set; }
    public string Notes { get; set; }
    public short StatusId { get; set; }
    public bool Default { get; set; }

}

