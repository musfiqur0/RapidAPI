namespace Domain.Models;

public class Status:Base
{
    public short Id { get; set; }
    public string Description { get; set; }
    public short StatusTypeId { get; set; }
    public StatusType StatusType { get; set; }
}
