using Domain.Models;

namespace Domain.Models;

public class Employee: Base
{
    public string Code { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Designation { get; set; }
    public decimal Salary { get; set; }
    public string Manager { get; set; }
    public string Location { get; set; }
    public short StatusId { get; set; }
    public Status Status { get; set; }
    public bool Default { get; set; }
    public string Action { get; set; }
}
