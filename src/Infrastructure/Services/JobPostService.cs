using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class JobPostService : IJobPostService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public JobPostService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.JobPosts
                .Include(e => e.Status)
                .AsQueryable();

            var totalCount = await entityQuery.CountAsync();
            var activeCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Active")
                .CountAsync();
            var inactiveCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Inactive")
                .CountAsync();
            var draftCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Draft")
                .CountAsync();
            var updatedCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Updated")
                .CountAsync();
            var deletedCount = await entityQuery
                .Where(e => e.Status != null && e.Status.Name == "Deleted")
                .CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                entityQuery = entityQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var entity = await (
                from e in entityQuery
                join l in _context.JobPostLocalizations on e.Id equals l.JobPostId
                select new
                {
                    id = e.Id,
                    companyId = e.CompanyId,
                    jobTitle = e.JobTitle,
                    jobCategoryId = e.JobCategoryId,
                    jobTypeId = e.JobTypeId,
                    noOfVacancies = e.NoOfVacancies,
                    closingDate = e.ClosingDate,
                    genderId = e.GenderId,
                    minimumExperienceId = e.MinimumExperienceId,
                    featured = e.Featured,
                    statusId = e.StatusId,
                    statusName = e.Status != null ? e.Status.Name : null,
                    shortDescription = e.ShortDescription,
                    longDescription = e.LongDescription,
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
                    Inactive = inactiveCount,
                    Draft = draftCount,
                    Updated = updatedCount,
                    Deleted = deletedCount
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


    public async Task<JobPostAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.JobPosts.FindAsync(id);
            var result = _mapper.Map<JobPostAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JobPostAddEditDto> CreateSingleAsync(JobPostAddEditDto dto)
    {
        try
        {
            var data = _mapper.Map<JobPost>(dto);
            _context.JobPosts.Add(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<JobPostAddEditDto>(data);

            var local = new JobPostLocalization
            {
                JobPostId = result.Id,
                Name = result.JobTitle,
                LanguageId = 1,
            };
            _context.JobPostLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new JobPostAudit
            {
                JobPostId = result.Id,
                CompanyId = result.CompanyId,
                JobTitle = result.JobTitle,
                JobCategoryId = result.JobCategoryId,
                JobTypeId = result.JobTypeId,
                NoOfVacancies = result.NoOfVacancies,
                ClosingDate = result.ClosingDate,
                GenderId = result.GenderId,
                MinimumExperienceId = result.MinimumExperienceId,
                Featured = result.Featured,
                StatusId = result.StatusId,
                ShortDescription = result.ShortDescription,
                LongDescription = result.LongDescription,
                Active = result.Active,

                // inherited from Audit base class
                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };

            _context.JobPostAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<JobPostAddEditDto>> CreateBulkAsync(IEnumerable<JobPostAddEditDto> dto)
    {
        try
        {
            List<JobPostAddEditDto> dataList = new List<JobPostAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.JobPosts.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JobPostAddEditDto> UpdateAsync(JobPostAddEditDto dto)
    {
        try
        {
            var data = await _context.JobPosts.FirstAsync(x => x.Id == dto.Id);
            if (data == null)
                throw new Exception("Something error");
            dto.Id = data.Id;
            _mapper.Map(dto, data);

            _context.JobPosts.Update(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<JobPostAddEditDto>(data);

            var audit = new JobPostAudit
            {
                JobPostId = result.Id,
                CompanyId = result.CompanyId,
                JobTitle = result.JobTitle,
                JobCategoryId = result.JobCategoryId,
                JobTypeId = result.JobTypeId,
                NoOfVacancies = result.NoOfVacancies,
                ClosingDate = result.ClosingDate,
                GenderId = result.GenderId,
                MinimumExperienceId = result.MinimumExperienceId,
                Featured = result.Featured,
                StatusId = result.StatusId,
                ShortDescription = result.ShortDescription,
                LongDescription = result.LongDescription,
                Active = result.Active,

                // inherited from Audit base class
                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.JobPostAudits.Add(audit);

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
            var result = await _context.JobPosts.FindAsync(id);
            if (result == null) return false;

            var audit = new JobPostAudit
            {
                JobPostId = result.Id,
                CompanyId = result.CompanyId,
                JobTitle = result.JobTitle,
                JobCategoryId = result.JobCategoryId,
                JobTypeId = result.JobTypeId,
                NoOfVacancies = result.NoOfVacancies,
                ClosingDate = result.ClosingDate,
                GenderId = result.GenderId,
                MinimumExperienceId = result.MinimumExperienceId,
                Featured = result.Featured,
                StatusId = result.StatusId,
                ShortDescription = result.ShortDescription,
                LongDescription = result.LongDescription,
                Active = result.Active,

                // inherited from Audit base class
                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.JobPostAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.JobPostLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.JobPostLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.JobPosts.Remove(result);
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
            // return static or predefined template data
            var data = new List<object>
            {
                new { Code = "101", JobTitle = "Software Engineer", JobTypeId = 1, StatusId = 1 },
                new { Code = "505", JobTitle = "Project Manager", JobTypeId = 2, StatusId = 1 }
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
            // return static or predefined template data
            var data = new List<object>
            {
                new { Id = "101", BannerImage = "C:\\Images\\JobPost\\SoftwareEngineer.png" },
                new { Id = "505", BannerImage = "C:\\Images\\JobPost\\ProjectManager.png" }
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
            var audits = _context.JobPostAudits
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

