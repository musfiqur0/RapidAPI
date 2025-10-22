using Domain.Models;

namespace Domain.Models;

public class EmployeeLocalization
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public string Name { get; set; } = null!;
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}
