namespace Domain.Models;

public class ServiceAudit : Audit
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public int ServiceCategoriesId { get; set; }
}

