namespace Domain.Models;

public class OvertimeTypeLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int OvertimeTypeId { get; set; }
    public OvertimeType OvertimeType { get; set; }
}

  