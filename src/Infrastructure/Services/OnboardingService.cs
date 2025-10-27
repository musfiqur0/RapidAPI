using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services;

public class OnboardingService : IOnboardingService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public OnboardingService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Onboardings
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery.Where(e => e.Draft == false).CountAsync(); // assuming draft=false means active
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
                join l in _context.OnboardingLocalizations on e.Id equals l.OnboardingId
                select new
                {
                    id = e.Id,
                    staffId = e.StaffId,
                    generalInformationId = e.GeneralInformationId,
                    staffFullName = e.StaffFullName,
                    address = e.Address,
                    assetAllocation = e.AssetAllocation,
                    typeOfTrainingId = e.TypeOfTrainingId,
                    trainingProgramId = e.TrainingProgramId,
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
                    Draft = draftCount,
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


    public async Task<OnboardingAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Onboardings.FindAsync(id);
            var result = _mapper.Map<OnboardingAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OnboardingAddEditDto> CreateSingleAsync(OnboardingAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Onboarding>(dto);
            _context.Onboardings.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<OnboardingAddEditDto>(entity);

            var local = new OnboardingLocalization
            {
                OnboardingId = result.Id,
                Name = result.StaffFullName,
                LanguageId = 1,
            };
            _context.OnboardingLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new OnboardingAudit
            {
                OnboardingId = result.Id,
                StaffId = result.StaffId,
                GeneralInformationId = result.GeneralInformationId,
                StaffFullName = result.StaffFullName,
                Address = result.Address,
                AssetAllocation = result.AssetAllocation,
                TypeOfTrainingId = result.TypeOfTrainingId,
                TrainingProgramId = result.TrainingProgramId,
                Default = result.Default,
                Draft = result.Draft,


                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.OnboardingAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<OnboardingAddEditDto>> CreateBulkAsync(IEnumerable<OnboardingAddEditDto> dto)
    {
        try
        {
            List<OnboardingAddEditDto> dataList = new List<OnboardingAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Onboardings.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OnboardingAddEditDto> UpdateAsync(OnboardingAddEditDto dto)
    {
        try
        {
            var entity = await _context.Onboardings.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Onboardings.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<OnboardingAddEditDto>(entity);

            var audit = new OnboardingAudit
            {
                OnboardingId = result.Id,
                StaffId = result.StaffId,
                GeneralInformationId = result.GeneralInformationId,
                StaffFullName = result.StaffFullName,
                Address = result.Address,
                AssetAllocation = result.AssetAllocation,
                TypeOfTrainingId = result.TypeOfTrainingId,
                TrainingProgramId = result.TrainingProgramId,
                Default = result.Default,
                Draft = result.Draft,


                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.OnboardingAudits.Add(audit);

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
            var result = await _context.Onboardings.FindAsync(id);
            if (result == null) return false;

            var audit = new OnboardingAudit
            {
                OnboardingId = result.Id,
                StaffId = result.StaffId,
                GeneralInformationId = result.GeneralInformationId,
                StaffFullName = result.StaffFullName,
                Address = result.Address,
                AssetAllocation = result.AssetAllocation,
                TypeOfTrainingId = result.TypeOfTrainingId,
                TrainingProgramId = result.TrainingProgramId,
                Default = result.Default,
                Draft = result.Draft,


                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.OnboardingAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.OnboardingLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.OnboardingLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Onboardings.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, OnboardingType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, OnboardingType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Onboarding model
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
            var audits = _context.OnboardingAudits
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
