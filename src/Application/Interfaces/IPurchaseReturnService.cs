using Application.DTOs;

namespace Application.Interfaces;

public interface IPurchaseReturnService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<PurchaseReturnAddEditDto?> GetSingleAsync(int id);
    Task<PurchaseReturnAddEditDto> CreateSingleAsync(PurchaseReturnAddEditDto dto);
    Task<IEnumerable<PurchaseReturnAddEditDto>> CreateBulkAsync(IEnumerable<PurchaseReturnAddEditDto> dto);
    Task<PurchaseReturnAddEditDto> UpdateAsync(PurchaseReturnAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
