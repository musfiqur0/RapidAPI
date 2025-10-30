using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TermsConditionsService : ITermsConditionsService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public TermsConditionsService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.TermsConditions
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
                join l in _context.TermsConditionsLocalizations on e.Id equals l.TermsConditionsId
                select new
                {
                    name = e.Name,
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


    public async Task<TermsConditionsAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.TermsConditions.FindAsync(id);
            var result = _mapper.Map<TermsConditionsAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TermsConditionsAddEditDto> CreateSingleAsync(TermsConditionsAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<TermsConditions>(dto);
            _context.TermsConditions.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TermsConditionsAddEditDto>(entity);

            var local = new TermsConditionsLocalization
            {
                TermsConditionsId = result.Id,
                Name = result.Name,
                LanguageId = 1,
            };
            _context.TermsConditionsLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new TermsConditionsAudit
            {
                Name = result.Name,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TermsConditionsAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<TermsConditionsAddEditDto>> CreateBulkAsync(IEnumerable<TermsConditionsAddEditDto> dto)
    {
        try
        {
            List<TermsConditionsAddEditDto> dataList = new List<TermsConditionsAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.TermsConditions.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TermsConditionsAddEditDto> UpdateAsync(TermsConditionsAddEditDto dto)
    {
        try
        {
            var entity = await _context.TermsConditions.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.TermsConditions.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TermsConditionsAddEditDto>(entity);

            var audit = new TermsConditionsAudit
            {
                Name = result.Name,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TermsConditionsAudits.Add(audit);

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
            var result = await _context.TermsConditions.FindAsync(id);
            if (result == null) return false;

            var audit = new TermsConditionsAudit
            {

                Name = result.Name,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TermsConditionsAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.TermsConditionsLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.TermsConditionsLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.TermsConditions.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, TermsConditionsType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, TermsConditionsType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in TermsConditions model
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
            var audits = _context.TermsConditionsAudits
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
