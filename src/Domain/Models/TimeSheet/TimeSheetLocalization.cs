namespace Domain.Models;

public class TimeSheetLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int TimeSheetId { get; set; }
    public TimeSheet TimeSheet { get; set; }
}
