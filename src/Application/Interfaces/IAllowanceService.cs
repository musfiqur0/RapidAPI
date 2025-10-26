using Application.DTOs;

namespace Application.Interfaces;

public interface IAllowanceService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<AllowanceAddEditDto?> GetSingleAsync(int id);
    Task<AllowanceAddEditDto> CreateSingleAsync(AllowanceAddEditDto dto);
    Task<IEnumerable<AllowanceAddEditDto>> CreateBulkAsync(IEnumerable<AllowanceAddEditDto> dto);
    Task<AllowanceAddEditDto> UpdateAsync(AllowanceAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
