using Domain.Models;

namespace Application.DTOs;

public class LeaveApplicationAddEditDto
{
    public int Id { get; set; } = 0;
    public int LeaveTypesId { get; set; }
    public string Notes { get; set; }
    public short StatusId { get; set; }
    public Status Status { get; set; }
    public bool Default { get; set; }

}