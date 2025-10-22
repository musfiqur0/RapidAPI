namespace Domain.Models;

public class GroupLocalization:Base
{
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}
