namespace Domain.Models;

public class WeeklyHolidayLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int WeeklyHolidayId { get; set; }
    public WeeklyHoliday WeeklyHoliday { get; set; }
}

