namespace Domain.Models;

public class SourceTypeAudit : Audit
{
    public int Id { get; set; }
    public int SourceTypeId { get; set; }
    public SourceType SourceType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
