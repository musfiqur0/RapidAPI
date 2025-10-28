using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Infrastructure.Services;

public class LeaveApplicationService : ILeaveApplicationService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public LeaveApplicationService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.LeaveApplications
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
                join l in _context.LeaveApplicationLocalizations on e.Id equals l.LeaveApplicationId
                select new
                {
                    id = e.Id,
                    leavetypesid = e.LeaveTypesId,
                    notes = e.Notes,
                    statusid = e.StatusId,
                    @default = e.Default,
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


    public async Task<LeaveApplicationAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.LeaveApplications.FindAsync(id);
            var result = _mapper.Map<LeaveApplicationAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LeaveApplicationAddEditDto> CreateSingleAsync(LeaveApplicationAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<LeaveApplication>(dto);
            _context.LeaveApplications.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<LeaveApplicationAddEditDto>(entity);

            var local = new LeaveApplicationLocalization
            {
                LeaveApplicationId = result.Id,
                Name = result.LeaveTypesId.ToString(),
                LanguageId = 1,
            };
            _context.LeaveApplicationLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new LeaveApplicationAudit
            {
                LeaveApplicationId = result.Id,
                LeaveTypesId = result.LeaveTypesId,
                Notes = result.Notes,
                StatusId = result.StatusId,
                Default = result.Default,

                // Audit-specific fields
                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded",
            };

            _context.LeaveApplicationAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<LeaveApplicationAddEditDto>> CreateBulkAsync(IEnumerable<LeaveApplicationAddEditDto> dto)
    {
        try
        {
            List<LeaveApplicationAddEditDto> dataList = new List<LeaveApplicationAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.LeaveApplications.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LeaveApplicationAddEditDto> UpdateAsync(LeaveApplicationAddEditDto dto)
    {
        try
        {
            var entity = await _context.LeaveApplications.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.LeaveApplications.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<LeaveApplicationAddEditDto>(entity);

            var audit = new LeaveApplicationAudit
            {
                LeaveApplicationId = result.Id,
                LeaveTypesId = result.LeaveTypesId,
                Notes = result.Notes,
                StatusId = result.StatusId,
                Default = result.Default,

                // Audit-specific fields
                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded",
            };
            _context.LeaveApplicationAudits.Add(audit);

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
            var result = await _context.LeaveApplications.FindAsync(id);
            if (result == null) return false;

            var audit = new LeaveApplicationAudit
            {
                LeaveApplicationId = result.Id,
                LeaveTypesId = result.LeaveTypesId,
                Notes = result.Notes,
                StatusId = result.StatusId,
                Default = result.Default,

                // Audit-specific fields
                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded",
            };
            _context.LeaveApplicationAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.LeaveApplicationLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.LeaveApplicationLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.LeaveApplications.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, LeaveApplicationType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, LeaveApplicationType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in LeaveApplication model
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
            var audits = _context.LeaveApplicationAudits
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
