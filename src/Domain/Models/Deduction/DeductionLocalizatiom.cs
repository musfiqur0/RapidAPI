namespace Domain.Models;

public class DeductionLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int DeductionId { get; set; }
    public Deduction Deduction { get; set; }
}