using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SourceTypeService : ISourceTypeService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public SourceTypeService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.SourceTypes
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
                join l in _context.SourceTypeLocalizations on e.Id equals l.SourceTypeId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    description = e.Description,
                    @default = e.Default,
                    draft = e.Draft,
                    localizationId = l != null ? l.Id : (int?)null
                }
            ).ToListAsync();

            var result = new
            {
                entity,
                statusCounter = new
                {
                    Total = totalCount
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


    public async Task<SourceTypeAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.SourceTypes.FindAsync(id);
            var result = _mapper.Map<SourceTypeAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SourceTypeAddEditDto> CreateSingleAsync(SourceTypeAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<SourceType>(dto);
            _context.SourceTypes.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<SourceTypeAddEditDto>(entity);

            var local = new SourceTypeLocalization
            {
                SourceTypeId = result.Id,
                Name = result.Description,
                LanguageId = 1,
            };
            _context.SourceTypeLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new SourceTypeAudit
            {
                SourceTypeId = result.Id,
                Name = result.Name,
                Description = result.Description,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.SourceTypeAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<SourceTypeAddEditDto>> CreateBulkAsync(IEnumerable<SourceTypeAddEditDto> dto)
    {
        try
        {
            List<SourceTypeAddEditDto> dataList = new List<SourceTypeAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.SourceTypes.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SourceTypeAddEditDto> UpdateAsync(SourceTypeAddEditDto dto)
    {
        try
        {
            var entity = await _context.SourceTypes.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.SourceTypes.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<SourceTypeAddEditDto>(entity);

            var audit = new SourceTypeAudit
            {
                SourceTypeId = result.Id,
                Name = result.Name,
                Description = result.Description,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.SourceTypeAudits.Add(audit);

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
            var result = await _context.SourceTypes.FindAsync(id);
            if (result == null) return false;

            var audit = new SourceTypeAudit
            {
                SourceTypeId = result.Id,
                Name = result.Name,
                Description = result.Description,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.SourceTypeAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.SourceTypeLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.SourceTypeLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.SourceTypes.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, SourceTypeType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, SourceTypeType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in SourceType model
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
            var audits = _context.SourceTypeAudits
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
