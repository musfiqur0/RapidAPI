namespace Domain.Models;

public class ServiceLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
}
