using Application.DTOs;

namespace Application.Interfaces;

public interface ITerminationService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<TerminationAddEditDto?> GetSingleAsync(int id);
    Task<TerminationAddEditDto> CreateSingleAsync(TerminationAddEditDto dto);
    Task<IEnumerable<TerminationAddEditDto>> CreateBulkAsync(IEnumerable<TerminationAddEditDto> dto);
    Task<TerminationAddEditDto> UpdateAsync(TerminationAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
