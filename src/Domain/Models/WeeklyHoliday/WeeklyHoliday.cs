namespace Domain.Models;

public class WeeklyHoliday : Base
{
    public DateTime FromDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
}
