using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PurchaseInvoiceService : IPurchaseInvoiceService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public PurchaseInvoiceService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.PurchaseInvoices
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
                join l in _context.PurchaseInvoiceLocalizations on e.Id equals l.PurchaseInvoiceId
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


    public async Task<PurchaseInvoiceAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.PurchaseInvoices.FindAsync(id);
            var result = _mapper.Map<PurchaseInvoiceAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PurchaseInvoiceAddEditDto> CreateSingleAsync(PurchaseInvoiceAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<PurchaseInvoice>(dto);
            _context.PurchaseInvoices.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<PurchaseInvoiceAddEditDto>(entity);

            var local = new PurchaseInvoiceLocalization
            {
                PurchaseInvoiceId = result.Id,
                Name = result.PONumber,
                LanguageId = 1,
            };
            _context.PurchaseInvoiceLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new PurchaseInvoiceAudit
            {
                PurchaseInvoiceId = result.Id,
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
            _context.PurchaseInvoiceAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<PurchaseInvoiceAddEditDto>> CreateBulkAsync(IEnumerable<PurchaseInvoiceAddEditDto> dto)
    {
        try
        {
            List<PurchaseInvoiceAddEditDto> dataList = new List<PurchaseInvoiceAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.PurchaseInvoices.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PurchaseInvoiceAddEditDto> UpdateAsync(PurchaseInvoiceAddEditDto dto)
    {
        try
        {
            var entity = await _context.PurchaseInvoices.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.PurchaseInvoices.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<PurchaseInvoiceAddEditDto>(entity);

            var audit = new PurchaseInvoiceAudit
            {
                PurchaseInvoiceId = result.Id,
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
            _context.PurchaseInvoiceAudits.Add(audit);

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
            var result = await _context.PurchaseInvoices.FindAsync(id);
            if (result == null) return false;

            var audit = new PurchaseInvoiceAudit
            {
                PurchaseInvoiceId = result.Id,
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
            _context.PurchaseInvoiceAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.PurchaseInvoiceLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.PurchaseInvoiceLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.PurchaseInvoices.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, PurchaseInvoiceType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, PurchaseInvoiceType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in PurchaseInvoice model
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
            var audits = _context.PurchaseInvoiceAudits
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
