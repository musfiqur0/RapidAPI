namespace Domain.Models;

public class LeavesApprovalAudit : Audit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LeavesApprovalId { get; set; }
    public int LeaveTypeId { get; set; }
    public int Employee { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
    public bool HardCopy { get; set; }
    public string ApprovedBy { get; set; }
    public short StatusId { get; set; }
    public Status Status { get; set; }
}
