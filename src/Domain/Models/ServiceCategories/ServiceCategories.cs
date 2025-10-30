namespace Domain.Models;

public class ServiceCategories : Base
{
    public string Description { get; set; }
    public ICollection<Service> Services { get; set; }
}
