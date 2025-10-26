using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TerminationService : ITerminationService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public TerminationService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Terminations
                .Include(e => e.Status)
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Active")
                .CountAsync();
            var inactiveCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Inactive")
                .CountAsync();
            var draftCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Draft")
                .CountAsync();
            var updatedCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Updated")
                .CountAsync();
            var deletedCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Deleted")
                .CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await (
                from e in entityQuery
                join l in _context.TerminationLocalizations on e.Id equals l.TerminationId
                select new
                {
                    id = e.Id,
                    date = e.Date,
                    iqamaNo = e.IqamaNo,
                    branchId = e.BranchId,
                    terminationType = e.TerminationType,
                    description = e.Description,
                    statusId = e.StatusId,
                    statusName = e.Status != null ? e.Status.Name : null,
                    localizationId = l != null ? l.Id : (int?)null
                }
            ).ToListAsync();

            var result = new
            {
                entity,
                statusCounter = new
                {
                    Total = totalCount,
                    Active = activeCount,
                    Inactive = inactiveCount,
                    Draft = draftCount,
                    Updated = updatedCount,
                    Deleted = deletedCount
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


    public async Task<TerminationAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Terminations.FindAsync(id);
            var result = _mapper.Map<TerminationAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TerminationAddEditDto> CreateSingleAsync(TerminationAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Termination>(dto);
            _context.Terminations.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TerminationAddEditDto>(entity);

            var local = new TerminationLocalization
            {
                TerminationId = result.Id,
                Name = result.Description,
                LanguageId = 1,
            };
            _context.TerminationLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new TerminationAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                TerminationType = result.TerminationType,
                Description = result.Description,
                StatusId = result.StatusId,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TerminationAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<TerminationAddEditDto>> CreateBulkAsync(IEnumerable<TerminationAddEditDto> dto)
    {
        try
        {
            List<TerminationAddEditDto> dataList = new List<TerminationAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Terminations.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TerminationAddEditDto> UpdateAsync(TerminationAddEditDto dto)
    {
        try
        {
            var entity = await _context.Terminations.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Terminations.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TerminationAddEditDto>(entity);

            var audit = new TerminationAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                TerminationType = result.TerminationType,
                Description = result.Description,
                StatusId = result.StatusId,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TerminationAudits.Add(audit);

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
            var result = await _context.Terminations.FindAsync(id);
            if (result == null) return false;

            var audit = new TerminationAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                TerminationType = result.TerminationType,
                Description = result.Description,
                StatusId = result.StatusId,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TerminationAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.TerminationLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.TerminationLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Terminations.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, TerminationType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, TerminationType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Termination model
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
            var audits = _context.TerminationAudits
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
