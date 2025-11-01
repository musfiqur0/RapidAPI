using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public PurchaseOrderService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.PurchaseOrders
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
                join l in _context.PurchaseOrderLocalizations on e.Id equals l.PurchaseOrderId
                select new
                {
                    id = e.Id,
                    documentnumber = e.DocumentNumber,
                    ponumber = e.PONumber,
                    podate = e.PODate,
                    supplierid = e.SupplierId,
                    paymentmodeid = e.PaymentModeId,
                    duedays = e.DueDays,
                    paymentdate = e.PaymentDate,
                    suppliernumber = e.SupplierNumber,
                    supplierstatusid = e.SupplierStatusId,
                    suppliergroupid = e.SupplierGroupId,
                    remarks = e.Remarks,
                    countryid = e.CountryId,
                    stateid = e.StateId,
                    cityid = e.CityId,
                    itemid = e.ItemId,
                    quantity = e.Quantity,
                    unitid = e.UnitId,
                    rate = e.Rate,
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


    public async Task<PurchaseOrderAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.PurchaseOrders.FindAsync(id);
            var result = _mapper.Map<PurchaseOrderAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PurchaseOrderAddEditDto> CreateSingleAsync(PurchaseOrderAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<PurchaseOrder>(dto);
            _context.PurchaseOrders.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<PurchaseOrderAddEditDto>(entity);

            var local = new PurchaseOrderLocalization
            {
                PurchaseOrderId = result.Id,
                Name = result.PONumber,
                LanguageId = 1,
            };
            _context.PurchaseOrderLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new PurchaseOrderAudit
            {
                PurchaseOrderId = result.Id,
                DocumentNumber = result.DocumentNumber,
                PONumber = result.PONumber,
                PODate = result.PODate,
                SupplierId = result.SupplierId,
                PaymentModeId = result.PaymentModeId,
                DueDays = result.DueDays,
                PaymentDate = result.PaymentDate,
                SupplierNumber = result.SupplierNumber,
                SupplierStatusId = result.SupplierStatusId,
                SupplierGroupId = result.SupplierGroupId,
                Remarks = result.Remarks,
                CountryId = result.CountryId,
                StateId = result.StateId,
                CityId = result.CityId,
                ItemId = result.ItemId,
                Quantity = result.Quantity,
                UnitId = result.UnitId,
                Rate = result.Rate,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.PurchaseOrderAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<PurchaseOrderAddEditDto>> CreateBulkAsync(IEnumerable<PurchaseOrderAddEditDto> dto)
    {
        try
        {
            List<PurchaseOrderAddEditDto> dataList = new List<PurchaseOrderAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.PurchaseOrders.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PurchaseOrderAddEditDto> UpdateAsync(PurchaseOrderAddEditDto dto)
    {
        try
        {
            var entity = await _context.PurchaseOrders.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.PurchaseOrders.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<PurchaseOrderAddEditDto>(entity);

            var audit = new PurchaseOrderAudit
            {
                PurchaseOrderId = result.Id,
                DocumentNumber = result.DocumentNumber,
                PONumber = result.PONumber,
                PODate = result.PODate,
                SupplierId = result.SupplierId,
                PaymentModeId = result.PaymentModeId,
                DueDays = result.DueDays,
                PaymentDate = result.PaymentDate,
                SupplierNumber = result.SupplierNumber,
                SupplierStatusId = result.SupplierStatusId,
                SupplierGroupId = result.SupplierGroupId,
                Remarks = result.Remarks,
                CountryId = result.CountryId,
                StateId = result.StateId,
                CityId = result.CityId,
                ItemId = result.ItemId,
                Quantity = result.Quantity,
                UnitId = result.UnitId,
                Rate = result.Rate,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.PurchaseOrderAudits.Add(audit);

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
            var result = await _context.PurchaseOrders.FindAsync(id);
            if (result == null) return false;

            var audit = new PurchaseOrderAudit
            {
                PurchaseOrderId = result.Id,
                DocumentNumber = result.DocumentNumber,
                PONumber = result.PONumber,
                PODate = result.PODate,
                SupplierId = result.SupplierId,
                PaymentModeId = result.PaymentModeId,
                DueDays = result.DueDays,
                PaymentDate = result.PaymentDate,
                SupplierNumber = result.SupplierNumber,
                SupplierStatusId = result.SupplierStatusId,
                SupplierGroupId = result.SupplierGroupId,
                Remarks = result.Remarks,
                CountryId = result.CountryId,
                StateId = result.StateId,
                CityId = result.CityId,
                ItemId = result.ItemId,
                Quantity = result.Quantity,
                UnitId = result.UnitId,
                Rate = result.Rate,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.PurchaseOrderAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.PurchaseOrderLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.PurchaseOrderLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.PurchaseOrders.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, PurchaseOrderType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, PurchaseOrderType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in PurchaseOrder model
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
            var audits = _context.PurchaseOrderAudits
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
