using Application.DTOs;

namespace Application.Interface
{
    public interface IEmployeeService
    {
        Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
        Task<EmployeeAddEditDto?> GetSingleAsync(int id);
        Task<EmployeeAddEditDto> CreateSingleAsync(EmployeeAddEditDto dto);
        Task<IEnumerable<EmployeeAddEditDto>> CreateBulkAsync(IEnumerable<EmployeeAddEditDto> dto);
        Task<EmployeeAddEditDto> UpdateAsync(EmployeeAddEditDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteBulkAsync(List<int> ids);
        Task<IEnumerable<object>> GetAllTemplateDataAsync();
        Task<object> GetAllGallaryAsync();
        Task<object> GetAllAuditsAsync();
    }
}
