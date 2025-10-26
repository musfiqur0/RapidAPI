namespace Domain.Models;

public class AllowanceLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int AllowanceId { get; set; }
    public Allowance Allowance { get; set; }
}