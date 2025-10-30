using Application.DTOs;

namespace Application.Interfaces;

public interface IServiceService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<ServiceAddEditDto?> GetSingleAsync(int id);
    Task<ServiceAddEditDto> CreateSingleAsync(ServiceAddEditDto dto);
    Task<IEnumerable<ServiceAddEditDto>> CreateBulkAsync(IEnumerable<ServiceAddEditDto> dto);
    Task<ServiceAddEditDto> UpdateAsync(ServiceAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}

