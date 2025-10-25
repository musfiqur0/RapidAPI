using Application.DTOs;

namespace Application.Interfaces;

public interface IIncrementService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<IncrementAddEditDto?> GetSingleAsync(int id);
    Task<IncrementAddEditDto> CreateSingleAsync(IncrementAddEditDto dto);
    Task<IEnumerable<IncrementAddEditDto>> CreateBulkAsync(IEnumerable<IncrementAddEditDto> dto);
    Task<IncrementAddEditDto> UpdateAsync(IncrementAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllAuditsAsync();
}