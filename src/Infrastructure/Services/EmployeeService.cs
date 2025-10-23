using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public EmployeeService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var employeesQuery = _context.Employees
                .Include(e => e.Status)
                .AsQueryable();

            var totalCount = await employeesQuery.CountAsync();
            var activeCount = await employeesQuery
                .Where(e => e.Status != null && e.Status.Name == "Active")
                .CountAsync();
            var inactiveCount = await employeesQuery
                .Where(e => e.Status != null && e.Status.Name == "Inactive")
                .CountAsync();
            var draftCount = await employeesQuery
                .Where(e => e.Status != null && e.Status.Name == "Draft")
                .CountAsync();
            var updatedCount = await employeesQuery
                .Where(e => e.Status != null && e.Status.Name == "Updated")
                .CountAsync();
            var deletedCount = await employeesQuery
                .Where(e => e.Status != null && e.Status.Name == "Deleted")
                .CountAsync();

            if (pageNumber > 0 && pageSize > 0)
            {
                employeesQuery = employeesQuery
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var employees = await (
                from e in employeesQuery
                join l in _context.EmployeeLocalizations on e.Id equals l.EmployeeId
                select new
                {
                    id = e.Id,
                    code = e.Code,
                    employeeName = !string.IsNullOrEmpty(l.Name) ? l.Name : e.Name,
                    email = e.Email,
                    phone = e.Phone,
                    department = e.Department,
                    designation = e.Designation,
                    salary = e.Salary,
                    manager = e.Manager,
                    location = e.Location,
                    statusId = e.StatusId,
                    statusName = e.Status != null ? e.Status.Name : null,
                    @default = e.Default,
                    action = e.Action,
                    localizationId = l.Id
                }
            ).ToListAsync();

            var result = new
            {
                employees,
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


    public async Task<EmployeeAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Employees.FindAsync(id);
            var resultedData = _mapper.Map<EmployeeAddEditDto>(obj);
            return resultedData;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<EmployeeAddEditDto> CreateSingleAsync(EmployeeAddEditDto dto)
    {
        try
        {
            var employee = _mapper.Map<Employee>(dto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var resultedData = _mapper.Map<EmployeeAddEditDto>(employee);

            var local = new EmployeeLocalization
            {
                EmployeeId = resultedData.Id,
                Name = resultedData.Name,
                LanguageId = 1,
            };
            _context.EmployeeLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new EmployeeAudit
            {
                Name = resultedData.Name,
                EmployeeId = resultedData.Id,
                Email = resultedData.Email,
                Phone = resultedData.Phone,
                Department = resultedData.Department,
                Designation = resultedData.Designation,
                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.EmployeeAudits.Add(audit);
            await _context.SaveChangesAsync();
            return resultedData;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<EmployeeAddEditDto>> CreateBulkAsync(IEnumerable<EmployeeAddEditDto> dto)
    {
        try
        {
            List<EmployeeAddEditDto> dataList = new List<EmployeeAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Employees.AddRange(employees);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<EmployeeAddEditDto> UpdateAsync(EmployeeAddEditDto dto)
    {
        try
        {
            var employee = await _context.Employees.FirstAsync(x => x.Id == dto.Id);
            if (employee == null)
                throw new Exception("Something error");
            dto.Id = employee.Id;
            _mapper.Map(dto, employee);

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            var resultedData = _mapper.Map<EmployeeAddEditDto>(employee);

            var audit = new EmployeeAudit
            {
                Name = resultedData.Name,
                EmployeeId = resultedData.Id,
                Email = resultedData.Email,
                Phone = resultedData.Phone,
                Department = resultedData.Department,
                Designation = resultedData.Designation,
                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"

            };
            _context.EmployeeAudits.Add(audit);

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
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            var audit = new EmployeeAudit
            {
                Name = employee.Name,
                EmployeeId = employee.Id,
                Email = employee.Email,
                Phone = employee.Phone,
                Department = employee.Department,
                Designation = employee.Designation,
                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"

            };
            _context.EmployeeAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.EmployeeLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.EmployeeLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Employees.Remove(employee);
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
            new { Department = "IT", Designation = "Developer" },
            new { Department = "HR", Designation = "Manager" }
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
            var audits = _context.EmployeeAudits
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
