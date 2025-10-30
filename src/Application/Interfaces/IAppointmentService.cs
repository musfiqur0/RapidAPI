using Application.DTOs;

namespace Application.Interfaces;

public interface IAppointmentService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<AppointmentAddEditDto?> GetSingleAsync(int id);
    Task<AppointmentAddEditDto> CreateSingleAsync(AppointmentAddEditDto dto);
    Task<IEnumerable<AppointmentAddEditDto>> CreateBulkAsync(IEnumerable<AppointmentAddEditDto> dto);
    Task<AppointmentAddEditDto> UpdateAsync(AppointmentAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
