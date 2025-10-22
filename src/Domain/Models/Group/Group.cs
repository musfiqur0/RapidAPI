namespace Domain.Models;

public class Group : Base
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public short StatusId { get; set; }
    public Status Status { get; set; }
    public bool Default { get; set; }
    public string Action { get; set; } = string.Empty;
}
