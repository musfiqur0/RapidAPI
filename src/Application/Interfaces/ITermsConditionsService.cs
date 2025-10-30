using Application.DTOs;

namespace Application.Interfaces;

public interface ITermsConditionsService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<TermsConditionsAddEditDto?> GetSingleAsync(int id);
    Task<TermsConditionsAddEditDto> CreateSingleAsync(TermsConditionsAddEditDto dto);
    Task<IEnumerable<TermsConditionsAddEditDto>> CreateBulkAsync(IEnumerable<TermsConditionsAddEditDto> dto);
    Task<TermsConditionsAddEditDto> UpdateAsync(TermsConditionsAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
