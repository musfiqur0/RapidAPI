namespace Domain.Models;

public class IncrementLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int IncrementId { get; set; }
    public Increment Increment { get; set; }
}