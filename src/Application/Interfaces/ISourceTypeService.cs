using Application.DTOs;

namespace Application.Interfaces;

public interface ISourceTypeService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<SourceTypeAddEditDto?> GetSingleAsync(int id);
    Task<SourceTypeAddEditDto> CreateSingleAsync(SourceTypeAddEditDto dto);
    Task<IEnumerable<SourceTypeAddEditDto>> CreateBulkAsync(IEnumerable<SourceTypeAddEditDto> dto);
    Task<SourceTypeAddEditDto> UpdateAsync(SourceTypeAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
