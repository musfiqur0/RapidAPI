using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;


    public AppointmentService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> GetAllAsync(int pageNumber = 0, int pageSize = 0)
    {
        try
        {
            var entityQuery = _context.Appointments
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
                join l in _context.AppointmentLocalizations on e.Id equals l.AppointmentId
                select new
                {
                    id = e.Id,
                    name = e.Name,
                    mobile = e.Mobile,
                    email = e.Email,
                    appointmentdate = e.AppointmentDate,
                    appointmenttime = e.AppointmentTime,
                    appointedbyid = e.AppointedById,

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


    public async Task<AppointmentAddEditDto?> GetSingleAsync(int id)
    {
        try
        {
            var obj = await _context.Appointments.FindAsync(id);
            var result = _mapper.Map<AppointmentAddEditDto>(obj);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AppointmentAddEditDto> CreateSingleAsync(AppointmentAddEditDto dto)
    {
        try
        {
            var entity = _mapper.Map<Appointment>(dto);
            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<AppointmentAddEditDto>(entity);

            var local = new AppointmentLocalization
            {
                AppointmentId = result.Id,
                Name = result.Name,
                LanguageId = 1,
            };
            _context.AppointmentLocalizations.Add(local);
            await _context.SaveChangesAsync();


            var audit = new AppointmentAudit
            {
                Name = result.Name,
                Mobile = result.Mobile,
                Email = result.Email,
                AppointmentDate = result.AppointmentDate,
                AppointmentTime = result.AppointmentTime,
                AppointedById = result.AppointedById,

                ActionTypeId = (short)EntityState.Added,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.AppointmentAudits.Add(audit);
            await _context.SaveChangesAsync();
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<AppointmentAddEditDto>> CreateBulkAsync(IEnumerable<AppointmentAddEditDto> dto)
    {
        try
        {
            List<AppointmentAddEditDto> dataList = new List<AppointmentAddEditDto>();
            foreach (var item in dto)
            {
                var insertedItem = await CreateSingleAsync(item);
                dataList.Add(insertedItem);
            }
            //_context.Appointments.AddRange(entity);
            return dataList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AppointmentAddEditDto> UpdateAsync(AppointmentAddEditDto dto)
    {
        try
        {
            var entity = await _context.Appointments.FirstAsync(x => x.Id == dto.Id);
            if (entity == null)
                throw new Exception("Something error");
            dto.Id = entity.Id;
            _mapper.Map(dto, entity);

            _context.Appointments.Update(entity);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<AppointmentAddEditDto>(entity);

            var audit = new AppointmentAudit
            {
                Name = result.Name,
                Mobile = result.Mobile,
                Email = result.Email,
                AppointmentDate = result.AppointmentDate,
                AppointmentTime = result.AppointmentTime,
                AppointedById = result.AppointedById,

                ActionTypeId = (short)EntityState.Modified,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.AppointmentAudits.Add(audit);

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
            var result = await _context.Appointments.FindAsync(id);
            if (result == null) return false;

            var audit = new AppointmentAudit
            {
                Name = result.Name,
                Mobile = result.Mobile,
                Email = result.Email,
                AppointmentDate = result.AppointmentDate,
                AppointmentTime = result.AppointmentTime,
                AppointedById = result.AppointedById,

                ActionTypeId = (short)EntityState.Deleted,
                Browser = "seeded",
                Location = "seeded",
                IP = "seeded",
                OS = "seeded",
                MapURL = "seeded"
            };
            _context.AppointmentAudits.Add(audit);
            await _context.SaveChangesAsync();


            var local = await _context.AppointmentLocalizations.FindAsync(id);
            if (local != null)
            {
                _context.AppointmentLocalizations.Remove(local);
                await _context.SaveChangesAsync();
            }

            _context.Appointments.Remove(result);
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
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 1234567890L, BranchId = 1, AppointmentType = "Voluntary", Description = "Resigned", StatusId = 1 },
            new { Date = DateTime.Now.ToString("yyyy-MM-dd"), IqamaNo = 9876543210L, BranchId = 2, AppointmentType = "Involuntary", Description = "Policy violation", StatusId = 2 }
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
            // No gallery/attachment fields in Appointment model
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
            var audits = _context.AppointmentAudits
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
