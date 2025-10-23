using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GroupService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var groupsQuery = _context.Groups
                .Include(e => e.Status)
                .AsQueryable();

            var totalCount = await groupsQuery.CountAsync();
            var activeCount = await groupsQuery
                .Where(e => e.Status != null && e.Status.Name == "Active")
                .CountAsync();
            var inactiveCount = await groupsQuery
                .Where(e => e.Status != null && e.Status.Name == "Inactive")
                .CountAsync();
            var draftCount = await groupsQuery
                .Where(e => e.Status != null && e.Status.Name == "Draft")
                .CountAsync();
            var updatedCount = await groupsQuery
                .Where(e => e.Status != null && e.Status.Name == "Updated")
                .CountAsync();
            var deletedCount = await groupsQuery
                .Where(e => e.Status != null && e.Status.Name == "Deleted")
                .CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                groupsQuery = groupsQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var groups = await (
                from e in groupsQuery
                join l in _context.GroupLocalizations on e.Id equals l.GroupId
                select new
                {
                    id = e.Id,
                    code = e.Code,
                    groupName = !string.IsNullOrEmpty(l.Name) ? l.Name : e.Name,
                    statusId = e.StatusId,
                    statusName = e.Status != null ? e.Status.Name : null,
                    @default = e.Default,
                    action = e.Action,
                    localizationId = l.Id
                }
            ).ToListAsync();

            var result = new
            {
                groups,
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


    public async Task<GroupAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Groups.FindAsync(id);
            var resultedData = _mapper.Map<GroupAddEditDto>(obj);
            return resultedData;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<GroupAddEditDto> CreateSingleAsync(GroupAddEditDto dto)
    {
        try
        {
            var group = _mapper.Map<Group>(dto);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            var resultedData = _mapper.Map<GroupAddEditDto>(group);

            var local = new GroupLocalization
            {
                GroupId = resultedData.Id,
                Name = resultedData.Name,
                LanguageId = 1,
            };
            _context.GroupLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new GroupAudit
            {
                Name = resultedData.Name,
                GroupId = resultedData.Id,
                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.GroupAudits.Add(audit);
            await _context.SaveChangesAsync();
            return resultedData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<GroupAddEditDto>> CreateBulkAsync(IEnumerable<GroupAddEditDto> dto)
    {
        try
        {
            List<GroupAddEditDto> dataList = new List<GroupAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Groups.AddRange(groups);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<GroupAddEditDto> UpdateAsync(GroupAddEditDto dto)
    {
        try
        {
            var group = await _context.Groups.FirstAsync(x => x.Id == dto.Id);
            if (group == null)
                throw new Exception("Something error");
            dto.Id = group.Id;
            _mapper.Map(dto, group);

            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            var resultedData = _mapper.Map<GroupAddEditDto>(group);

            var audit = new GroupAudit
            {
                Name = resultedData.Name,
                GroupId = resultedData.Id,
                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"

            };
            _context.GroupAudits.Add(audit);

            await _context.SaveChangesAsync();
            return resultedData;
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
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return false;

            var audit = new GroupAudit
            {
                Name = group.Name,
                GroupId = group.Id,
                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"

            };
            _context.GroupAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.GroupLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.GroupLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Groups.Remove(group);
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
            new { Code = "101"},
            new { Code = "505"}
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
                new { Id = "101", ProfilePhoto = "C:\\Users\\Avenger\\Pictures\\Screenshots\\Screenshot (1).png" },
                new { Id = "505", ProfilePhoto = "C:\\Users\\Avenger\\Pictures\\Screenshots\\Screenshot (1).png" },
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
            var audits = _context.GroupAudits
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
