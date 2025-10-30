using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public ServiceService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Services
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
                join l in _context.ServiceLocalizations on e.Id equals l.ServiceId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    servicecategoriesid = e.ServiceCategoriesId,

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


    public async Task<ServiceAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Services.FindAsync(id);
            var result = _mapper.Map<ServiceAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ServiceAddEditDto> CreateSingleAsync(ServiceAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Service>(dto);
            _context.Services.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ServiceAddEditDto>(entity);

            var local = new ServiceLocalization
            {
                ServiceId = result.Id,
                Name = result.ServiceCategoriesId.ToString(),
                LanguageId = 1,
            };
            _context.ServiceLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new ServiceAudit
            {
                Name = result.Name,
                ServiceCategoriesId = result.ServiceCategoriesId,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.ServiceAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ServiceAddEditDto>> CreateBulkAsync(IEnumerable<ServiceAddEditDto> dto)
    {
        try
        {
            List<ServiceAddEditDto> dataList = new List<ServiceAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Services.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ServiceAddEditDto> UpdateAsync(ServiceAddEditDto dto)
    {
        try
        {
            var entity = await _context.Services.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Services.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ServiceAddEditDto>(entity);

            var audit = new ServiceAudit
            {
                Name = result.Name,
                ServiceCategoriesId = result.ServiceCategoriesId,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.ServiceAudits.Add(audit);

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
            var result = await _context.Services.FindAsync(id);
            if (result == null) return false;

            var audit = new ServiceAudit
            {
                Name = result.Name,
                ServiceCategoriesId = result.ServiceCategoriesId,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.ServiceAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.ServiceLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.ServiceLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Services.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, ServiceType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, ServiceType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Service model
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
            var audits = _context.ServiceAudits
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
