using Domain.Models;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Domain.Models;

public class Appointment : Base
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime AppointmentTime { get; set; }
    public int AppointedById { get; set; }

}