using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IJobPostService
{
    Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0);
    Task<JobPostAddEditDto?> GetSingleAsync(int id);
    Task<JobPostAddEditDto> CreateSingleAsync(JobPostAddEditDto dto);
    Task<IEnumerable<JobPostAddEditDto>> CreateBulkAsync(IEnumerable<JobPostAddEditDto> dto);
    Task<JobPostAddEditDto> UpdateAsync(JobPostAddEditDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteBulkAsync(List<int> ids);
    Task<IEnumerable<object>> GetAllTemplateDataAsync();
    Task<object> GetAllGallaryAsync();
    Task<object> GetAllAuditsAsync();
}
