namespace Domain.Models;

public class TerminationLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int TerminationId { get; set; }
    public Termination Termination { get; set; }
}