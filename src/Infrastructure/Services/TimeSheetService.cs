using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TimeSheetService : ITimeSheetService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public TimeSheetService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.TimeSheets
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
                join l in _context.TimeSheetLocalizations on e.Id equals l.TimeSheetId
                select new
                {
                    id = e.Id,
                    branch = e.Branch,
                    iqamano = e.IqamaNo,
                    employeename = e.EmployeeName,
                    designation = e.Designation,
                    actualhours = e.ActualHours,
                    overtimehours = e.OvertimeHours,
                    totalhours = e.TotalHours,
                    absenthours = e.AbsentHours,
                    nethours = e.NetHours,
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


    public async Task<TimeSheetAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.TimeSheets.FindAsync(id);
            var result = _mapper.Map<TimeSheetAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TimeSheetAddEditDto> CreateSingleAsync(TimeSheetAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<TimeSheet>(dto);
            _context.TimeSheets.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TimeSheetAddEditDto>(entity);

            var local = new TimeSheetLocalization
            {
                TimeSheetId = result.Id,
                Name = result.Branch,
                LanguageId = 1,
            };
            _context.TimeSheetLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new TimeSheetAudit
            {
                TimeSheetId = result.Id,
                Branch = result.Branch,
                IqamaNo = result.IqamaNo,
                EmployeeName = result.EmployeeName,
                Designation = result.Designation,
                ActualHours = result.ActualHours,
                OvertimeHours = result.OvertimeHours,
                TotalHours = result.TotalHours,
                AbsentHours = result.AbsentHours,
                NetHours = result.NetHours,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TimeSheetAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<TimeSheetAddEditDto>> CreateBulkAsync(IEnumerable<TimeSheetAddEditDto> dto)
    {
        try
        {
            List<TimeSheetAddEditDto> dataList = new List<TimeSheetAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.TimeSheets.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TimeSheetAddEditDto> UpdateAsync(TimeSheetAddEditDto dto)
    {
        try
        {
            var entity = await _context.TimeSheets.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.TimeSheets.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<TimeSheetAddEditDto>(entity);

            var audit = new TimeSheetAudit
            {
                TimeSheetId = result.Id,
                Branch = result.Branch,
                IqamaNo = result.IqamaNo,
                EmployeeName = result.EmployeeName,
                Designation = result.Designation,
                ActualHours = result.ActualHours,
                OvertimeHours = result.OvertimeHours,
                TotalHours = result.TotalHours,
                AbsentHours = result.AbsentHours,
                NetHours = result.NetHours,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TimeSheetAudits.Add(audit);

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
            var result = await _context.TimeSheets.FindAsync(id);
            if (result == null) return false;

            var audit = new TimeSheetAudit
            {
                TimeSheetId = result.Id,
                Branch = result.Branch,
                IqamaNo = result.IqamaNo,
                EmployeeName = result.EmployeeName,
                Designation = result.Designation,
                ActualHours = result.ActualHours,
                OvertimeHours = result.OvertimeHours,
                TotalHours = result.TotalHours,
                AbsentHours = result.AbsentHours,
                NetHours = result.NetHours,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.TimeSheetAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.TimeSheetLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.TimeSheetLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.TimeSheets.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, TimeSheetType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, TimeSheetType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in TimeSheet model
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
            var audits = _context.TimeSheetAudits
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
