using Application.DTOs;

namespace Application.Interfaces;

public interface ITimeSheetService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<TimeSheetAddEditDto?> GetSingleAsync(int id);
    Task<TimeSheetAddEditDto> CreateSingleAsync(TimeSheetAddEditDto dto);
    Task<IEnumerable<TimeSheetAddEditDto>> CreateBulkAsync(IEnumerable<TimeSheetAddEditDto> dto);
    Task<TimeSheetAddEditDto> UpdateAsync(TimeSheetAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
