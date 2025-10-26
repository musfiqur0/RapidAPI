namespace Domain.Models;

public class BenefitPenaltyLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int BenefitPenaltyId { get; set; }
    public BenefitPenalty BenefitPenalty { get; set; }
}