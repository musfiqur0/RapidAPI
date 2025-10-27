using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services;

public class DeductionService : IDeductionService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public DeductionService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Deductions
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var defaultCount = await entityQuery.Where(e => e.Default).CountAsync();
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
                join l in _context.DeductionLocalizations on e.Id equals l.DeductionId
                select new
                {
                    id = e.Id,
                    date = e.Date,
                    iqamaNo = e.IqamaNo,
                    branchId = e.BranchId,
                    deductionTypeId = e.DeductionTypeId,
                    deductionAmount = e.DeductionAmount,
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
                    Default = defaultCount,
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


    public async Task<DeductionAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Deductions.FindAsync(id);
            var result = _mapper.Map<DeductionAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<DeductionAddEditDto> CreateSingleAsync(DeductionAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Deduction>(dto);
            _context.Deductions.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<DeductionAddEditDto>(entity);

            var local = new DeductionLocalization
            {
                DeductionId = result.Id,
                Name = result.DeductionTypeId.ToString(),
                LanguageId = 1,
            };
            _context.DeductionLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new DeductionAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                DeductionTypeId = result.DeductionTypeId,
                DeductionAmount = result.DeductionAmount,
                Notes = result.Notes,
                Default = result.Default,
                Draft = result.Draft,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.DeductionAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<DeductionAddEditDto>> CreateBulkAsync(IEnumerable<DeductionAddEditDto> dto)
    {
        try
        {
            List<DeductionAddEditDto> dataList = new List<DeductionAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Deductions.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<DeductionAddEditDto> UpdateAsync(DeductionAddEditDto dto)
    {
        try
        {
            var entity = await _context.Deductions.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Deductions.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<DeductionAddEditDto>(entity);

            var audit = new DeductionAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                DeductionTypeId = result.DeductionTypeId,
                DeductionAmount = result.DeductionAmount,
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

            _context.DeductionAudits.Add(audit);

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
            var result = await _context.Deductions.FindAsync(id);
            if (result == null) return false;

            var audit = new DeductionAudit
            {
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                DeductionTypeId = result.DeductionTypeId,
                DeductionAmount = result.DeductionAmount,
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

            _context.DeductionAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.DeductionLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.DeductionLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Deductions.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, DeductionType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, DeductionType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Deduction model
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
            var audits = _context.DeductionAudits
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
