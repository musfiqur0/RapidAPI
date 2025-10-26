using Application.DTOs;

namespace Application.Interfaces;

public interface ICandidateSelectionService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<CandidateSelectionAddEditDto?> GetSingleAsync(int id);
    Task<CandidateSelectionAddEditDto> CreateSingleAsync(CandidateSelectionAddEditDto dto);
    Task<IEnumerable<CandidateSelectionAddEditDto>> CreateBulkAsync(IEnumerable<CandidateSelectionAddEditDto> dto);
    Task<CandidateSelectionAddEditDto> UpdateAsync(CandidateSelectionAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllAuditsAsync();
}