using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Services;

public class PaymentModeService : IPaymentModeService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PaymentModeService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var query = _context.PaymentModes.AsQueryable();

            var totalCount = await query.CountAsync();
            var activeCount = await query.Where(e => e.Draft == false).CountAsync(); // assuming draft=false means active
            var draftCount = await query.Where(e => e.Draft == true).CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var data = await (
                from e in query
                join l in _context.PaymentModeLocalizations on e.Id equals l.PaymentModeId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    bankAccounts = e.BankAccounts,
                    @default = e.Default,
                    draft = e.Draft,
                    localizationId = l.Id
                }
            ).ToListAsync();

            var result = new
            {
                paymentModes = data,
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

    public async Task<PaymentModeAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.PaymentModes.FindAsync(id);
            var result = _mapper.Map<PaymentModeAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PaymentModeAddEditDto> CreateSingleAsync(PaymentModeAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<PaymentMode>(dto);
            _context.PaymentModes.Add(entity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PaymentModeAddEditDto>(entity);

            var localization = new PaymentModeLocalization
            {
                PaymentModeId = result.Id,
                Name = result.Name,
                LanguageId = 1
            };
            _context.PaymentModeLocalizations.Add(localization);
            await _context.SaveChangesAsync();

            var audit = new PaymentModeAudit
            {
                PaymentModeId = result.Id,
                Name = result.Name,
                BankAccounts = result.BankAccounts,
                Default = result.Default,
                Draft = result.Draft,
                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.PaymentModeAudits.Add(audit);
            await _context.SaveChangesAsync();

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<PaymentModeAddEditDto>> CreateBulkAsync(IEnumerable<PaymentModeAddEditDto> dtos)
    {
        try
        {
            List<PaymentModeAddEditDto> resultList = new List<PaymentModeAddEditDto>();
            foreach (var dto in dtos)
            {
                var inserted = await CreateSingleAsync(dto);
                resultList.Add(inserted);
            }
            return resultList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PaymentModeAddEditDto> UpdateAsync(PaymentModeAddEditDto dto)
    {
        try
        {
            var entity = await _context.PaymentModes.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Record not found");

            _mapper.Map(dto, entity);
            _context.PaymentModes.Update(entity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PaymentModeAddEditDto>(entity);

            var audit = new PaymentModeAudit
            {
                PaymentModeId = result.Id,
                Name = result.Name,
                BankAccounts = result.BankAccounts,
                Default = result.Default,
                Draft = result.Draft,
                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.PaymentModeAudits.Add(audit);
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
            var entity = await _context.PaymentModes.FindAsync(id);
            if (entity == null) return false;

            var audit = new PaymentModeAudit
            {
                PaymentModeId = entity.Id,
                Name = entity.Name,
                BankAccounts = entity.BankAccounts,
                Default = entity.Default,
                Draft = entity.Draft,
                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.PaymentModeAudits.Add(audit);
            await _context.SaveChangesAsync();

            var localization = await _context.PaymentModeLocalizations
                .FirstOrDefaultAsync(x => x.PaymentModeId == id);
            if (localization != null)
            {
                _context.PaymentModeLocalizations.Remove(localization);
                await _context.SaveChangesAsync();
            }

            _context.PaymentModes.Remove(entity);
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
                new { Name = "Cash" },
                new { Name = "Bank Transfer" }
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
            var data = new List<object>
            {
                new { Id = "1", Icon = "C:\\Assets\\Payment\\cash.png" },
                new { Id = "2", Icon = "C:\\Assets\\Payment\\bank.png" }
            }.AsEnumerable();

            return data;
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
            var audits = _context.PaymentModeAudits
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