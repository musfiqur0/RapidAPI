namespace Domain.Models;

public class AllowanceTypeAudit : Audit
{
    public int Id { get; set; }
    public int AllowanceTypeId { get; set; }
    public AllowanceType AllowanceType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
