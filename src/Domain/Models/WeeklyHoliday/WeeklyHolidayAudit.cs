using Domain.Models;
namespace Domain.Models;

public class WeeklyHolidayAudit : Audit
{
    public int Id { get; set; }
    public int WeeklyHolidayId { get; set; }
    public string Name { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }


}
