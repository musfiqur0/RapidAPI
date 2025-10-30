namespace Domain.Models;

public class ServiceCategoriesLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int ServiceCategoriesId { get; set; }
    public ServiceCategories ServiceCategories { get; set; }
}
