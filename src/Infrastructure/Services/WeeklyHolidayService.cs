using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class WeeklyHolidayService : IWeeklyHolidayService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public WeeklyHolidayService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.WeeklyHolidays
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
                join l in _context.WeeklyHolidayLocalizations on e.Id equals l.WeeklyHolidayId
                select new
                {
                    id= e.Id,
                    name= e.Name,
                    fromdate= e.FromDate,
                    enddate= e.EndDate,
                    totaldays= e.TotalDays,
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


    public async Task<WeeklyHolidayAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.WeeklyHolidays.FindAsync(id);
            var result = _mapper.Map<WeeklyHolidayAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<WeeklyHolidayAddEditDto> CreateSingleAsync(WeeklyHolidayAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<WeeklyHoliday>(dto);
            _context.WeeklyHolidays.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<WeeklyHolidayAddEditDto>(entity);

            var local = new WeeklyHolidayLocalization
            {
                WeeklyHolidayId = result.Id,
                Name = result.Id.ToString(),
                LanguageId = 1,
            };
            _context.WeeklyHolidayLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new WeeklyHolidayAudit
            {
                WeeklyHolidayId = result.Id,
                Name = result.Name,
                FromDate = result.FromDate,
                EndDate = result.EndDate,
                TotalDays = result.TotalDays,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.WeeklyHolidayAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<WeeklyHolidayAddEditDto>> CreateBulkAsync(IEnumerable<WeeklyHolidayAddEditDto> dto)
    {
        try
        {
            List<WeeklyHolidayAddEditDto> dataList = new List<WeeklyHolidayAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.WeeklyHolidays.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<WeeklyHolidayAddEditDto> UpdateAsync(WeeklyHolidayAddEditDto dto)
    {
        try
        {
            var entity = await _context.WeeklyHolidays.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.WeeklyHolidays.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<WeeklyHolidayAddEditDto>(entity);

            var audit = new WeeklyHolidayAudit
            {
                WeeklyHolidayId = result.Id,
                Name = result.Name,
                FromDate = result.FromDate,
                EndDate = result.EndDate,
                TotalDays = result.TotalDays,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.WeeklyHolidayAudits.Add(audit);

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
            var result = await _context.WeeklyHolidays.FindAsync(id);
            if (result == null) return false;

            var audit = new WeeklyHolidayAudit
            {
                WeeklyHolidayId = result.Id,
                Name = result.Name,
                FromDate = result.FromDate,
                EndDate = result.EndDate,
                TotalDays = result.TotalDays,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.WeeklyHolidayAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.WeeklyHolidayLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.WeeklyHolidayLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.WeeklyHolidays.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, WeeklyHolidayType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, WeeklyHolidayType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in WeeklyHoliday model
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
            var audits = _context.WeeklyHolidayAudits
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
