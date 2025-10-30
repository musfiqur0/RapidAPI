using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ServiceCategoriesService : IServiceCategoriesService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public ServiceCategoriesService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.ServiceCategories
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await (
                from e in entityQuery
                join l in _context.ServiceCategoriesLocalizations on e.Id equals l.ServiceCategoriesId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    description = e.Description,
                    localizationId = l != null ? l.Id : (int?)null
                }
            ).ToListAsync();

            var result = new
            {
                entity,
                statusCounter = new
                {
                    Total = totalCount,
                },
                pagination = (pageNumber > 0 && pageSize > 0)
                    ? new
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                    }
                    : null
            };

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<ServiceCategoriesAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.ServiceCategories.FindAsync(id);
            var result = _mapper.Map<ServiceCategoriesAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ServiceCategoriesAddEditDto> CreateSingleAsync(ServiceCategoriesAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<ServiceCategories>(dto);
            _context.ServiceCategories.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ServiceCategoriesAddEditDto>(entity);

            var local = new ServiceCategoriesLocalization
            {
                ServiceCategoriesId = result.Id,
                Name = result.Description,
                LanguageId = 1,
            };
            _context.ServiceCategoriesLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new ServiceCategoriesAudit
            {
                ServiceCategoriesId = result.Id,
                Name = result.Name,
                Description = result.Description,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.ServiceCategoriesAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ServiceCategoriesAddEditDto>> CreateBulkAsync(IEnumerable<ServiceCategoriesAddEditDto> dto)
    {
        try
        {
            List<ServiceCategoriesAddEditDto> dataList = new List<ServiceCategoriesAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.ServiceCategories.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ServiceCategoriesAddEditDto> UpdateAsync(ServiceCategoriesAddEditDto dto)
    {
        try
        {
            var entity = await _context.ServiceCategories.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.ServiceCategories.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ServiceCategoriesAddEditDto>(entity);

            var audit = new ServiceCategoriesAudit
            {
                ServiceCategoriesId = result.Id,
                Name = result.Name,
                Description = result.Description,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.ServiceCategoriesAudits.Add(audit);

            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var result = await _context.ServiceCategories.FindAsync(id);
            if (result == null) return false;

            var audit = new ServiceCategoriesAudit
            {
                ServiceCategoriesId = result.Id,
                Name = result.Name,
                Description = result.Description,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.ServiceCategoriesAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.ServiceCategoriesLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.ServiceCategoriesLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.ServiceCategories.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteBulkAsync(List<int> ids)
    {
        try
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id);
            }
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<IEnumerable<object>> GetAllTemplateDataAsync()
    {
        try
        {
            var data = new List<object>
        {
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, ServiceCategoriesType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, ServiceCategoriesType = "Involuntary", Description = "Policy violation", StatusId = 2 }
        }.AsEnumerable();

            return Task.FromResult(data);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<object> GetAllGallaryAsync()
    {
        try
        {
            // No gallery/attachment fields in ServiceCategories model
            return new List<object>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<object> GetAllAuditsAsync()
    {
        try
        {
            var audits = _context.ServiceCategoriesAudits
                                .OrderByDescending(x => x.Id)
                                .ToList();
            return audits;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
