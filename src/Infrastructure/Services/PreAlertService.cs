using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PreAlertService : IPreAlertService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PreAlertService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.PreAlerts
                .Include(e => e.Status)
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery.Where(e => e.Active).CountAsync();
            var inactiveCount = await entityQuery.Where(e => !e.Active).CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await (
                from e in entityQuery
                join l in _context.PreAlertLocalizations on e.Id equals l.PreAlertId
                select new
                {
                    id = e.Id,
                    tracking = e.Tracking,
                    date = e.Date,
                    customer = e.Customer,
                    shippingCompanyId = e.ShippingCompanyId,
                    supplierId = e.SupplierId,
                    packageDescription = e.PackageDescription,
                    deliveryDate = e.DeliveryDate,
                    purchasePrice = e.PurchasePrice,
                    statusId = e.StatusId,
                    statusName = e.Status != null ? e.Status.Name : null,
                    active = e.Active,
                    localizationId = l.Id
                }
            ).ToListAsync();

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

    public async Task<PreAlertAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.PreAlerts.FindAsync(id);
            var result = _mapper.Map<PreAlertAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PreAlertAddEditDto> CreateSingleAsync(PreAlertAddEditDto dto)
    {
        try
        {
            var data = _mapper.Map<PreAlert>(dto);
            _context.PreAlerts.Add(data);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PreAlertAddEditDto>(data);

            var local = new PreAlertLocalization
            {
                PreAlertId = data.Id,
                Name = data.Tracking,
                LanguageId = 1
            };
            _context.PreAlertLocalizations.Add(local);
            await _context.SaveChangesAsync();

            var audit = new PreAlertAudit
            {
                Tracking = data.Tracking,
                Date = data.Date,
                Customer = data.Customer,
                ShippingCompanyId = data.ShippingCompanyId,
                SupplierId = data.SupplierId,
                PackageDescription = data.PackageDescription,
                DeliveryDate = data.DeliveryDate,
                PurchasePrice = data.PurchasePrice,
                StatusId = data.StatusId,
                Active = data.Active,

                // inherited from Audit base class
                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.PreAlertAudits.Add(audit);
            await _context.SaveChangesAsync();

            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<PreAlertAddEditDto>> CreateBulkAsync(IEnumerable<PreAlertAddEditDto> dto)
    {
        try
        {
            List<PreAlertAddEditDto> dataList = new();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PreAlertAddEditDto> UpdateAsync(PreAlertAddEditDto dto)
    {
        try
        {
            var data = await _context.PreAlerts.FirstAsync(x => x.Id == dto.Id);
            if (data == null)
                throw new Exception("PreAlert not found");

            _mapper.Map(dto, data);
            _context.PreAlerts.Update(data);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PreAlertAddEditDto>(data);

            var audit = new PreAlertAudit
            {
                Tracking = data.Tracking,
                Date = data.Date,
                Customer = data.Customer,
                ShippingCompanyId = data.ShippingCompanyId,
                SupplierId = data.SupplierId,
                PackageDescription = data.PackageDescription,
                DeliveryDate = data.DeliveryDate,
                PurchasePrice = data.PurchasePrice,
                StatusId = data.StatusId,
                Active = data.Active,

                // inherited from Audit base class
                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.PreAlertAudits.Add(audit);
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
            var result = await _context.PreAlerts.FindAsync(id);
            if (result == null) return false;

            var audit = new PreAlertAudit
            {
                Tracking = result.Tracking,
                Date = result.Date,
                Customer = result.Customer,
                ShippingCompanyId = result.ShippingCompanyId,
                SupplierId = result.SupplierId,
                PackageDescription = result.PackageDescription,
                DeliveryDate = result.DeliveryDate,
                PurchasePrice = result.PurchasePrice,
                StatusId = result.StatusId,
                Active = result.Active,

                // inherited from Audit base class
                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.PreAlertAudits.Add(audit);
            await _context.SaveChangesAsync();

            var local = await _context.PreAlertLocalizations.FirstOrDefaultAsync(x => x.PreAlertId == id);
            if (local != null)
            {
                _context.PreAlertLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.PreAlerts.Remove(result);
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
                new { Tracking = "TRK123", Customer = "John Doe", ShippingCompanyId = 1, StatusId = 1 },
                new { Tracking = "TRK456", Customer = "Jane Smith", ShippingCompanyId = 2, StatusId = 2 }
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
            var audits = _context.PreAlertAudits
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
