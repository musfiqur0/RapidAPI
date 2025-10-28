using Application.DTOs;

namespace Application.Interfaces;

public interface IOvertimeTypeService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<OvertimeTypeAddEditDto?> GetSingleAsync(int id);
    Task<OvertimeTypeAddEditDto> CreateSingleAsync(OvertimeTypeAddEditDto dto);
    Task<IEnumerable<OvertimeTypeAddEditDto>> CreateBulkAsync(IEnumerable<OvertimeTypeAddEditDto> dto);
    Task<OvertimeTypeAddEditDto> UpdateAsync(OvertimeTypeAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
