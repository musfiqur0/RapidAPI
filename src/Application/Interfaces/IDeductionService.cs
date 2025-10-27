using Application.DTOs;

namespace Application.Interfaces;

public interface IDeductionService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<DeductionAddEditDto?> GetSingleAsync(int id);
    Task<DeductionAddEditDto> CreateSingleAsync(DeductionAddEditDto dto);
    Task<IEnumerable<DeductionAddEditDto>> CreateBulkAsync(IEnumerable<DeductionAddEditDto> dto);
    Task<DeductionAddEditDto> UpdateAsync(DeductionAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
