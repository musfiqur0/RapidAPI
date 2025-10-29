using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class LeavesApprovalService : ILeavesApprovalService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public LeavesApprovalService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.LeavesApprovals
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
                join l in _context.LeavesApprovalLocalizations on e.Id equals l.LeavesApprovalId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    leavetypeid = e.LeaveTypeId,
                    employee = e.Employee,
                    fromdate = e.FromDate,
                    enddate = e.EndDate,
                    totaldays = e.TotalDays,
                    hardcopy = e.HardCopy,
                    approvedby = e.ApprovedBy,
                    statusid = e.StatusId,

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


    public async Task<LeavesApprovalAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.LeavesApprovals.FindAsync(id);
            var result = _mapper.Map<LeavesApprovalAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LeavesApprovalAddEditDto> CreateSingleAsync(LeavesApprovalAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<LeavesApproval>(dto);
            _context.LeavesApprovals.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<LeavesApprovalAddEditDto>(entity);

            var local = new LeavesApprovalLocalization
            {
                LeavesApprovalId = result.Id,
                Name = result.Name,
                LanguageId = 1,
            };
            _context.LeavesApprovalLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new LeavesApprovalAudit
            {
                Name = result.Name,
                LeaveTypeId = result.LeaveTypeId,
                Employee = result.Employee,
                FromDate = result.FromDate,
                EndDate = result.EndDate,
                TotalDays = result.TotalDays,
                HardCopy = result.HardCopy,
                ApprovedBy = result.ApprovedBy,
                StatusId = result.StatusId,


                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.LeavesApprovalAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<LeavesApprovalAddEditDto>> CreateBulkAsync(IEnumerable<LeavesApprovalAddEditDto> dto)
    {
        try
        {
            List<LeavesApprovalAddEditDto> dataList = new List<LeavesApprovalAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.LeavesApprovals.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LeavesApprovalAddEditDto> UpdateAsync(LeavesApprovalAddEditDto dto)
    {
        try
        {
            var entity = await _context.LeavesApprovals.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.LeavesApprovals.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<LeavesApprovalAddEditDto>(entity);

            var audit = new LeavesApprovalAudit
            {
                Name = result.Name,
                LeaveTypeId = result.LeaveTypeId,
                Employee = result.Employee,
                FromDate = result.FromDate,
                EndDate = result.EndDate,
                TotalDays = result.TotalDays,
                HardCopy = result.HardCopy,
                ApprovedBy = result.ApprovedBy,
                StatusId = result.StatusId,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.LeavesApprovalAudits.Add(audit);

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
            var result = await _context.LeavesApprovals.FindAsync(id);
            if (result == null) return false;

            var audit = new LeavesApprovalAudit
            {
                Name = result.Name,
                LeaveTypeId = result.LeaveTypeId,
                Employee = result.Employee,
                FromDate = result.FromDate,
                EndDate = result.EndDate,
                TotalDays = result.TotalDays,
                HardCopy = result.HardCopy,
                ApprovedBy = result.ApprovedBy,
                StatusId = result.StatusId,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.LeavesApprovalAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.LeavesApprovalLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.LeavesApprovalLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.LeavesApprovals.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, LeavesApprovalType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, LeavesApprovalType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in LeavesApproval model
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
            var audits = _context.LeavesApprovalAudits
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
