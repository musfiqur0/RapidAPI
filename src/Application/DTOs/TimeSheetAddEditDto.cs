namespace Application.DTOs;

public class TimeSheetAddEditDto
{
    public int Id { get; set; } = 0;
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
