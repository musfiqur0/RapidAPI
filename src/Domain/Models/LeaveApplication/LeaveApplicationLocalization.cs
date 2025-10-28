namespace Domain.Models;

public class LeaveApplicationLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int LeaveApplicationId { get; set; }
    public LeaveApplication LeaveApplication { get; set; }
}
