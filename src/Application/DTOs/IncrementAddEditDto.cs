namespace Application.DTOs;

public class IncrementAddEditDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "Text";
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public bool Default { get; set; }
    public bool Draft { get; set; }
}