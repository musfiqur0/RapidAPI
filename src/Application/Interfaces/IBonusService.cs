using Application.DTOs;

namespace Application.Interfaces;

public interface IBonusService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<BonusAddEditDto?> GetSingleAsync(int id);
    Task<BonusAddEditDto> CreateSingleAsync(BonusAddEditDto dto);
    Task<IEnumerable<BonusAddEditDto>> CreateBulkAsync(IEnumerable<BonusAddEditDto> dto);
    Task<BonusAddEditDto> UpdateAsync(BonusAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
