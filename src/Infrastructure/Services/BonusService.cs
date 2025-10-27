using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BonusService : IBonusService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public BonusService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Bonuss
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
                join l in _context.BonusLocalizations on e.Id equals l.BonusId
                select new
                {
                    id = e.Id,
                    date = e.Date,
                    iqamaNo = e.IqamaNo,
                    branchId = e.BranchId,
                    bonusTypeId = e.BonusTypeId,
                    bonusAmount = e.BonusAmount,
                    note = e.Note,
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


    public async Task<BonusAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Bonuss.FindAsync(id);
            var result = _mapper.Map<BonusAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<BonusAddEditDto> CreateSingleAsync(BonusAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Bonus>(dto);
            _context.Bonuss.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<BonusAddEditDto>(entity);

            var local = new BonusLocalization
            {
                BonusId = result.Id,
                Name = result.BonusTypeId,
                LanguageId = 1,
            };
            _context.BonusLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new BonusAudit
            {
                BonusId = result.Id,
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                BonusTypeId = result.BonusTypeId,
                BonusAmount = result.BonusAmount,
                Note = result.Note,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.BonusAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<BonusAddEditDto>> CreateBulkAsync(IEnumerable<BonusAddEditDto> dto)
    {
        try
        {
            List<BonusAddEditDto> dataList = new List<BonusAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Bonuss.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<BonusAddEditDto> UpdateAsync(BonusAddEditDto dto)
    {
        try
        {
            var entity = await _context.Bonuss.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Bonuss.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<BonusAddEditDto>(entity);

            var audit = new BonusAudit
            {
                BonusId = result.Id,
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                BonusTypeId = result.BonusTypeId,
                BonusAmount = result.BonusAmount,
                Note = result.Note,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.BonusAudits.Add(audit);

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
            var result = await _context.Bonuss.FindAsync(id);
            if (result == null) return false;

            var audit = new BonusAudit
            {
                BonusId = result.Id,
                Date = result.Date,
                IqamaNo = result.IqamaNo,
                BranchId = result.BranchId,
                BonusTypeId = result.BonusTypeId,
                BonusAmount = result.BonusAmount,
                Note = result.Note,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.BonusAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.BonusLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.BonusLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Bonuss.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, BonusType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, BonusType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Bonus model
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
            var audits = _context.BonusAudits
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
