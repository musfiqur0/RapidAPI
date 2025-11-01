using Application.DTOs;

namespace Application.Interfaces;

public interface IPurchaseInvoiceService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<PurchaseInvoiceAddEditDto?> GetSingleAsync(int id);
    Task<PurchaseInvoiceAddEditDto> CreateSingleAsync(PurchaseInvoiceAddEditDto dto);
    Task<IEnumerable<PurchaseInvoiceAddEditDto>> CreateBulkAsync(IEnumerable<PurchaseInvoiceAddEditDto> dto);
    Task<PurchaseInvoiceAddEditDto> UpdateAsync(PurchaseInvoiceAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
