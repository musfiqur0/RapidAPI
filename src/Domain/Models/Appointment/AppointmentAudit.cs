using Domain.Models;
namespace Domain.Models;

public class AppointmentAudit : Audit
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime AppointmentTime { get; set; }
    public int AppointedById { get; set; }

}
