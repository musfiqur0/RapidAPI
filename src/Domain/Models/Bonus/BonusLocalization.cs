namespace Domain.Models;

public class BonusLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int BonusId { get; set; }
    public Bonus Bonus { get; set; }
}