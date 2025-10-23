using Application.DTOs;

namespace Application.Interfaces;

public interface IPaymentModeService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<PaymentModeAddEditDto?> GetSingleAsync(int id);
    Task<PaymentModeAddEditDto> CreateSingleAsync(PaymentModeAddEditDto dto);
    Task<IEnumerable<PaymentModeAddEditDto>> CreateBulkAsync(IEnumerable<PaymentModeAddEditDto> dto);
    Task<PaymentModeAddEditDto> UpdateAsync(PaymentModeAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
