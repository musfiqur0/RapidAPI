namespace Domain.Models;

public class CandidateSelectionLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int CandidateSelectionId { get; set; }
    public CandidateSelection CandidateSelection { get; set; }
}