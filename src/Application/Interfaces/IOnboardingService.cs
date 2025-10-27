using Application.DTOs;

namespace Application.Interfaces;

public interface IOnboardingService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<OnboardingAddEditDto?> GetSingleAsync(int id);
    Task<OnboardingAddEditDto> CreateSingleAsync(OnboardingAddEditDto dto);
    Task<IEnumerable<OnboardingAddEditDto>> CreateBulkAsync(IEnumerable<OnboardingAddEditDto> dto);
    Task<OnboardingAddEditDto> UpdateAsync(OnboardingAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
