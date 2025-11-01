using Application.DTOs;

namespace Application.Interfaces;

public interface IPurchaseOrderService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<PurchaseOrderAddEditDto?> GetSingleAsync(int id);
    Task<PurchaseOrderAddEditDto> CreateSingleAsync(PurchaseOrderAddEditDto dto);
    Task<IEnumerable<PurchaseOrderAddEditDto>> CreateBulkAsync(IEnumerable<PurchaseOrderAddEditDto> dto);
    Task<PurchaseOrderAddEditDto> UpdateAsync(PurchaseOrderAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
