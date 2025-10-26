namespace Application.DTOs;

public class TerminationAddEditDto
{
    public int Id { get; set; } = 0;
    public DateTime Date { get; set; }
    public long IqamaNo { get; set; }
    public int BranchId { get; set; }
    public string TerminationType { get; set; }
    public string Description { get; set; }
    public short StatusId { get; set; }
}