namespace Domain.Models;

public class TimeSheetAudit : Audit
{
    public int Id { get; set; }
    public int TimeSheetId { get; set; }
    public string Branch { get; set; }
    public long IqamaNo { get; set; }
    public string EmployeeName { get; set; }
    public string Designation { get; set; }
    public decimal ActualHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal TotalHours { get; set; }
    public decimal AbsentHours { get; set; }
    public decimal NetHours { get; set; }
}
