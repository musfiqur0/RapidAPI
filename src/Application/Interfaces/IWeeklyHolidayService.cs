using Application.DTOs;

namespace Application.Interfaces;

public interface IWeeklyHolidayService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<WeeklyHolidayAddEditDto?> GetSingleAsync(int id);
    Task<WeeklyHolidayAddEditDto> CreateSingleAsync(WeeklyHolidayAddEditDto dto);
    Task<IEnumerable<WeeklyHolidayAddEditDto>> CreateBulkAsync(IEnumerable<WeeklyHolidayAddEditDto> dto);
    Task<WeeklyHolidayAddEditDto> UpdateAsync(WeeklyHolidayAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
