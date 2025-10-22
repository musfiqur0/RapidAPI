using Application.DTOs;

namespace Application.Interface;

public interface IGroupService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<GroupAddEditDto?> GetSingleAsync(int id);
    Task<GroupAddEditDto> CreateSingleAsync(GroupAddEditDto dto);
    Task<IEnumerable<GroupAddEditDto>> CreateBulkAsync(IEnumerable<GroupAddEditDto> dto);
    Task<GroupAddEditDto> UpdateAsync(GroupAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
