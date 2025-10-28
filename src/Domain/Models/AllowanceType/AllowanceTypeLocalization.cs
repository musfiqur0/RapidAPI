namespace Domain.Models;

public class AllowanceTypeLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int AllowanceTypeId { get; set; }
    public AllowanceType AllowanceType { get; set; }
}
