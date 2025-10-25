using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services;

public class IncrementService : IIncrementService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public IncrementService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Increments.AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery.Where(e => !e.Draft).CountAsync();
            var draftCount = await entityQuery.Where(e => e.Draft).CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await (
                from e in entityQuery
                join l in _context.IncrementLocalizations on e.Id equals l.IncrementId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    date = e.Date,
                    amount = e.Amount,
                    note = e.Note,
                    employeeId = e.EmployeeId,
                    branch = e.Branch,
                    @default = e.Default,
                    draft = e.Draft,
                    localizationId = l.Id
                }
            ).ToListAsync();

            var result = new
            {
                entity,
                statusCounter = new
                {
                    Total = totalCount,
                    Active = activeCount,
                    Draft = draftCount
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

    public async Task<IncrementAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Increments.FindAsync(id);
            return _mapper.Map<IncrementAddEditDto>(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IncrementAddEditDto> CreateSingleAsync(IncrementAddEditDto dto)
    {
        try
        {
            var data = _mapper.Map<Increment>(dto);
            _context.Increments.Add(data);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<IncrementAddEditDto>(data);

            var local = new IncrementLocalization
            {
                IncrementId = data.Id,
                Name = data.Name,
                LanguageId = 1
            };
            _context.IncrementLocalizations.Add(local);
            await _context.SaveChangesAsync();

            var audit = new IncrementAudit
            {
                Name = data.Name,
                Date = data.Date,
                Amount = data.Amount,
                Note = data.Note,
                EmployeeId = data.EmployeeId,
                Branch = data.Branch,
                Default = data.Default,
                Draft = data.Draft,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.IncrementAudits.Add(audit);
            await _context.SaveChangesAsync();

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<IncrementAddEditDto>> CreateBulkAsync(IEnumerable<IncrementAddEditDto> dto)
    {
        var results = new List<IncrementAddEditDto>();
        foreach (var item in dto)
        {
            var created = await CreateSingleAsync(item);
            results.Add(created);
        }
        return results;
    }

    public async Task<IncrementAddEditDto> UpdateAsync(IncrementAddEditDto dto)
    {
        try
        {
            var data = await _context.Increments.FirstAsync(x => x.Id == dto.Id);
            if (data == null) throw new Exception("Increment not found");

            _mapper.Map(dto, data);
            _context.Increments.Update(data);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<IncrementAddEditDto>(data);

            var audit = new IncrementAudit
            {
                Name = data.Name,
                Date = data.Date,
                Amount = data.Amount,
                Note = data.Note,
                EmployeeId = data.EmployeeId,
                Branch = data.Branch,
                Default = data.Default,
                Draft = data.Draft,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.IncrementAudits.Add(audit);
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
            var result = await _context.Increments.FindAsync(id);
            if (result == null) return false;

            var audit = new IncrementAudit
            {
                Name = result.Name,
                Date = result.Date,
                Amount = result.Amount,
                Note = result.Note,
                EmployeeId = result.EmployeeId,
                Branch = result.Branch,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.IncrementAudits.Add(audit);
            await _context.SaveChangesAsync();

            var local = await _context.IncrementLocalizations.FirstOrDefaultAsync(x => x.IncrementId == id);
            if (local != null)
            {
                _context.IncrementLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Increments.Remove(result);
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
        foreach (var id in ids)
        {
            await DeleteAsync(id);
        }
        return true;
    }

    public Task<IEnumerable<object>> GetAllTemplateDataAsync()
    {
        var data = new List<object>
        {
            new { Name = "Bonus Q3", Date = DateTime.Now, Amount = 500m, EmployeeId = 1, Branch = "HQ", Default = false, Draft = true },
            new { Name = "Adjustment", Date = DateTime.Now.AddDays(-5), Amount = 200m, EmployeeId = 2, Branch = "Branch A", Default = true, Draft = false }
        }.AsEnumerable();

        return Task.FromResult(data);
    }

    public async Task<object> GetAllAuditsAsync()
    {
        try
        {
            var audits = await _context.IncrementAudits
                .OrderByDescending(x => x.Id)
                .ToListAsync();
            return audits;
        }
        catch (Exception)
        {
            throw;
        }
    }
}