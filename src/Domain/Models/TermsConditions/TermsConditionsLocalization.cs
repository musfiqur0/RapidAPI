namespace Domain.Models;

public class TermsConditionsLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int TermsConditionsId { get; set; }
    public TermsConditions TermsConditions { get; set; }
}