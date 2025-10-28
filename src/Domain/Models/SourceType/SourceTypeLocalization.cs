namespace Domain.Models;

public class SourceTypeLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int SourceTypeId { get; set; }
    public SourceType SourceType { get; set; }
}