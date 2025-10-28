using Application.DTOs;

namespace Application.Interfaces;

public interface IAllowanceTypeService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<AllowanceTypeAddEditDto?> GetSingleAsync(int id);
    Task<AllowanceTypeAddEditDto> CreateSingleAsync(AllowanceTypeAddEditDto dto);
    Task<IEnumerable<AllowanceTypeAddEditDto>> CreateBulkAsync(IEnumerable<AllowanceTypeAddEditDto> dto);
    Task<AllowanceTypeAddEditDto> UpdateAsync(AllowanceTypeAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
