using Application.DTOs;

namespace Application.Interfaces;

public interface IBenefitPenaltyService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<BenefitPenaltyAddEditDto?> GetSingleAsync(int id);
    Task<BenefitPenaltyAddEditDto> CreateSingleAsync(BenefitPenaltyAddEditDto dto);
    Task<IEnumerable<BenefitPenaltyAddEditDto>> CreateBulkAsync(IEnumerable<BenefitPenaltyAddEditDto> dto);
    Task<BenefitPenaltyAddEditDto> UpdateAsync(BenefitPenaltyAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
