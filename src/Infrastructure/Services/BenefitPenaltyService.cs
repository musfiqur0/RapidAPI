using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services;

public class BenefitPenaltyService : IBenefitPenaltyService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public BenefitPenaltyService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.BenefitPenaltys
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
                join l in _context.BenefitPenaltyLocalizations on e.Id equals l.BenefitPenaltyId
                select new
                {
                    id = e.Id,
                    typeId = e.TypeId,
                    subjectId = e.SubjectId,
                    criteria = e.Criteria,
                    date = e.Date,
                    driverId = e.DriverId,
                    formalityId = e.FormalityId,
                    description = e.Description,
                    localizationId = l != null ? l.Id : (int?)null
                }
            ).ToListAsync();

            var result = new
            {
                entity,
                statusCounter = new
                {
                    Total = totalCount
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


    public async Task<BenefitPenaltyAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.BenefitPenaltys.FindAsync(id);
            var result = _mapper.Map<BenefitPenaltyAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<BenefitPenaltyAddEditDto> CreateSingleAsync(BenefitPenaltyAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<BenefitPenalty>(dto);
            _context.BenefitPenaltys.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<BenefitPenaltyAddEditDto>(entity);

            var local = new BenefitPenaltyLocalization
            {
                BenefitPenaltyId = result.Id,
                Name = result.Description,
                LanguageId = 1,
            };
            _context.BenefitPenaltyLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new BenefitPenaltyAudit
            {
                Date = result.Date,
                TypeId = result.TypeId,
                SubjectId = result.SubjectId,
                Criteria = result.Criteria,
                DriverId = result.DriverId,
                FormalityId = result.FormalityId,
                Description = result.Description,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.BenefitPenaltyAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<BenefitPenaltyAddEditDto>> CreateBulkAsync(IEnumerable<BenefitPenaltyAddEditDto> dto)
    {
        try
        {
            List<BenefitPenaltyAddEditDto> dataList = new List<BenefitPenaltyAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.BenefitPenaltys.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<BenefitPenaltyAddEditDto> UpdateAsync(BenefitPenaltyAddEditDto dto)
    {
        try
        {
            var entity = await _context.BenefitPenaltys.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.BenefitPenaltys.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<BenefitPenaltyAddEditDto>(entity);

            var audit = new BenefitPenaltyAudit
            {
                Date = result.Date,
                TypeId = result.TypeId,
                SubjectId = result.SubjectId,
                Criteria = result.Criteria,
                DriverId = result.DriverId,
                FormalityId = result.FormalityId,
                Description = result.Description,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.BenefitPenaltyAudits.Add(audit);

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
            var result = await _context.BenefitPenaltys.FindAsync(id);
            if (result == null) return false;

            var audit = new BenefitPenaltyAudit
            {
                Date = result.Date,
                TypeId = result.TypeId,
                SubjectId = result.SubjectId,
                Criteria = result.Criteria,
                DriverId = result.DriverId,
                FormalityId = result.FormalityId,
                Description = result.Description,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.BenefitPenaltyAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.BenefitPenaltyLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.BenefitPenaltyLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.BenefitPenaltys.Remove(result);
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
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var data = new List<object>
        {
            new { Date = today, TypeId = 1, SubjectId = 101, Criteria = "Late arrival > 3 times/week", DriverId = 501, FormalityId = 1, Description = "Repeated tardiness - verbal warning" },
            new { Date = today, TypeId = 2, SubjectId = 205, Criteria = "Exceeded sales target by 20%", DriverId = 502, FormalityId = 2, Description = "Q3 performance bonus approved" }
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
            // No gallery/attachment fields in BenefitPenalty model
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
            var audits = _context.BenefitPenaltyAudits
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

