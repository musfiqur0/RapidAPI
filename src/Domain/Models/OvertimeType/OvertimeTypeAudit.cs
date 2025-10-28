namespace Domain.Models;

public class OvertimeTypeAudit : Audit
{
    public int Id { get; set; }
    public int OvertimeTypeId { get; set; }
    public OvertimeType OvertimeType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
