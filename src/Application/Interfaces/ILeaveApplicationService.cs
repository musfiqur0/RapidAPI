using Application.DTOs;

namespace Application.Interfaces;

public interface ILeaveApplicationService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<LeaveApplicationAddEditDto?> GetSingleAsync(int id);
    Task<LeaveApplicationAddEditDto> CreateSingleAsync(LeaveApplicationAddEditDto dto);
    Task<IEnumerable<LeaveApplicationAddEditDto>> CreateBulkAsync(IEnumerable<LeaveApplicationAddEditDto> dto);
    Task<LeaveApplicationAddEditDto> UpdateAsync(LeaveApplicationAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
