namespace Domain.Models;

public class ActionType
{
    public short Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
