namespace Application.DTOs;

public class CandidateSelectionAddEditDto
{
    public int Id { get; set; } = 0;
    public string EmployeeName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
    public bool Default { get; set; }
    public bool Draft { get; set; }
}