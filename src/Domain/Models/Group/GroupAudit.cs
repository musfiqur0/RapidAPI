namespace Domain.Models;

public class GroupAudit : Audit
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
