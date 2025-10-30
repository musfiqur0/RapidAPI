namespace Domain.Models;

public class Service : Base
{
    public int ServiceCategoriesId { get; set; }
    public ServiceCategories ServiceCategories { get; set; }
}
