namespace Domain.Models;

public class ServiceCategoriesAudit : Audit
{
    public int Id { get; set; }
    public int ServiceCategoriesId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
