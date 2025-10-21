using Domain.Models;
namespace Domain.Model;

public class EmployeeAudit: Audit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EmployeeId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;

    //public Employee Employee { get; set; }

}
