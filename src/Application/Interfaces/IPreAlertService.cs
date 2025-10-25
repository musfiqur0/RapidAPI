using Application.DTOs;

namespace Application.Interfaces;


public interface IPreAlertService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<PreAlertAddEditDto?> GetSingleAsync(int id);
    Task<PreAlertAddEditDto> CreateSingleAsync(PreAlertAddEditDto dto);
    Task<IEnumerable<PreAlertAddEditDto>> CreateBulkAsync(IEnumerable<PreAlertAddEditDto> dto);
    Task<PreAlertAddEditDto> UpdateAsync(PreAlertAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllAuditsAsync();
}
