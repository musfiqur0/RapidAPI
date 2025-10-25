namespace Domain.Models;

public class Group : Base
{
    public string Code { get; set; }
    public string Description { get; set; }
    public short StatusId { get; set; }
    public Status Status { get; set; }
    public bool Default { get; set; }
    public string Action { get; set; }
}
