using Domain.Models;
namespace Domain.Models;

public class EmployeeAudit: Audit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EmployeeId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Department { get; set; }
    public string Designation { get; set; }

    //public Employee Employee { get; set; }

}
