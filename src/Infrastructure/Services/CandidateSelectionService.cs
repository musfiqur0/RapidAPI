using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CandidateSelectionService : ICandidateSelectionService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CandidateSelectionService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.CandidateSelections.AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery.Where(e => !e.Draft).CountAsync();
            var inactiveCount = await entityQuery.Where(e => e.Draft).CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await entityQuery.Select(e => new
            {
                id = e.Id,
                employeeName = e.EmployeeName,
                position = e.Position,
                team = e.Team,
                @default = e.Default,
                draft = e.Draft,
                isActive = !e.Draft
            }).ToListAsync();

            var result = new
            {
                entity,
                statusCounter = new
                {
                    Total = totalCount,
                    Active = activeCount,
                    Inactive = inactiveCount
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

    public async Task<CandidateSelectionAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.CandidateSelections.FindAsync(id);
            return _mapper.Map<CandidateSelectionAddEditDto>(obj);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CandidateSelectionAddEditDto> CreateSingleAsync(CandidateSelectionAddEditDto dto)
    {
        try
        {
            var data = _mapper.Map<CandidateSelection>(dto);
            _context.CandidateSelections.Add(data);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<CandidateSelectionAddEditDto>(data);

            // Create audit record
            var audit = new CandidateSelectionAudit
            {
                EmployeeName = data.EmployeeName,
                Position = data.Position,
                Team = data.Team,
                Default = data.Default,
                Draft = data.Draft,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.CandidateSelectionAudits.Add(audit);
            await _context.SaveChangesAsync();

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<CandidateSelectionAddEditDto>> CreateBulkAsync(IEnumerable<CandidateSelectionAddEditDto> dto)
    {
        try
        {
            var results = new List<CandidateSelectionAddEditDto>();
            foreach (var item in dto)
            {
                var created = await CreateSingleAsync(item);
                results.Add(created);
            }
            return results;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CandidateSelectionAddEditDto> UpdateAsync(CandidateSelectionAddEditDto dto)
    {
        try
        {
            var existing = await _context.CandidateSelections.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (existing == null)
                throw new Exception("CandidateSelection not found");

            _mapper.Map(dto, existing);
            _context.CandidateSelections.Update(existing);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<CandidateSelectionAddEditDto>(existing);

            // Audit log
            var audit = new CandidateSelectionAudit
            {
                EmployeeName = existing.EmployeeName,
                Position = existing.Position,
                Team = existing.Team,
                Default = existing.Default,
                Draft = existing.Draft,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.CandidateSelectionAudits.Add(audit);
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
            var entity = await _context.CandidateSelections.FindAsync(id);
            if (entity == null) return false;

            // Audit log for deletion
            var audit = new CandidateSelectionAudit
            {
                EmployeeName = entity.EmployeeName,
                Position = entity.Position,
                Team = entity.Team,
                Default = entity.Default,
                Draft = entity.Draft,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.CandidateSelectionAudits.Add(audit);
            await _context.SaveChangesAsync();

            _context.CandidateSelections.Remove(entity);
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
                new { EmployeeName = "John Doe", Position = "Developer", Team = "Engineering", Default = false, Draft = false },
                new { EmployeeName = "Jane Smith", Position = "Manager", Team = "HR", Default = true, Draft = false }
            }.AsEnumerable();

            return Task.FromResult(data);
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
            var audits = await _context.CandidateSelectionAudits
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