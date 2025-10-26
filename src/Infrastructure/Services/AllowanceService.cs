using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services;

public class AllowanceService : IAllowanceService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public AllowanceService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Allowances
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery.Where(e => e.Draft == false).CountAsync();
            var draftCount = await entityQuery.Where(e => e.Draft == true).CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await (
                from e in entityQuery
                join l in _context.AllowanceLocalizations on e.Id equals l.AllowanceId
                select new
                {
                    id = e.Id,
                    date = e.Date,
                    iqamaNo = e.IqamaNo,
                    branchId = e.BranchId,
                    allowanceTypeId = e.AllowanceTypeId,
                    allowanceAmount = e.AllowanceAmount,
                    notes = e.Notes,
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


    public async Task<AllowanceAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Allowances.FindAsync(id);
            var result = _mapper.Map<AllowanceAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AllowanceAddEditDto> CreateSingleAsync(AllowanceAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Allowance>(dto);
            _context.Allowances.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<AllowanceAddEditDto>(entity);

            var local = new AllowanceLocalization
            {
                AllowanceId = result.Id,
                Name = result.AllowanceTypeId.ToString(),
                LanguageId = 1,
            };
            _context.AllowanceLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new AllowanceAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                AllowanceTypeId = result.AllowanceTypeId,
                AllowanceAmount = result.AllowanceAmount,
                Notes = result.Notes,
                Default = result.Default,
                Draft  = result.Draft,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.AllowanceAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<AllowanceAddEditDto>> CreateBulkAsync(IEnumerable<AllowanceAddEditDto> dto)
    {
        try
        {
            List<AllowanceAddEditDto> dataList = new List<AllowanceAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Allowances.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AllowanceAddEditDto> UpdateAsync(AllowanceAddEditDto dto)
    {
        try
        {
            var entity = await _context.Allowances.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Allowances.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<AllowanceAddEditDto>(entity);

            var audit = new AllowanceAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                AllowanceTypeId = result.AllowanceTypeId,
                AllowanceAmount = result.AllowanceAmount,
                Notes = result.Notes,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.AllowanceAudits.Add(audit);

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
            var result = await _context.Allowances.FindAsync(id);
            if (result == null) return false;

            var audit = new AllowanceAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                AllowanceTypeId = result.AllowanceTypeId,
                AllowanceAmount = result.AllowanceAmount,
                Notes = result.Notes,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.AllowanceAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.AllowanceLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.AllowanceLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Allowances.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, AllowanceType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, AllowanceType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Allowance model
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
            var audits = _context.AllowanceAudits
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

