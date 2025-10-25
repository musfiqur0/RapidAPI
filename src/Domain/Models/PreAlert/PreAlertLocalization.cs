namespace Domain.Models;

public class PreAlertLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int PreAlertId { get; set; }
    public PreAlert PreAlert { get; set; }
}