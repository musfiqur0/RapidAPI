namespace Domain.Models;

public class StatusType
{
    public short Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
