using Application.DTOs;

namespace Application.Interfaces;

public interface IServiceCategoriesService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<ServiceCategoriesAddEditDto?> GetSingleAsync(int id);
    Task<ServiceCategoriesAddEditDto> CreateSingleAsync(ServiceCategoriesAddEditDto dto);
    Task<IEnumerable<ServiceCategoriesAddEditDto>> CreateBulkAsync(IEnumerable<ServiceCategoriesAddEditDto> dto);
    Task<ServiceCategoriesAddEditDto> UpdateAsync(ServiceCategoriesAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}