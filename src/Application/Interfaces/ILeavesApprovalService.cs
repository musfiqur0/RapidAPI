using Application.DTOs;

namespace Application.Interfaces;

public interface ILeavesApprovalService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<LeavesApprovalAddEditDto?> GetSingleAsync(int id);
    Task<LeavesApprovalAddEditDto> CreateSingleAsync(LeavesApprovalAddEditDto dto);
    Task<IEnumerable<LeavesApprovalAddEditDto>> CreateBulkAsync(IEnumerable<LeavesApprovalAddEditDto> dto);
    Task<LeavesApprovalAddEditDto> UpdateAsync(LeavesApprovalAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
