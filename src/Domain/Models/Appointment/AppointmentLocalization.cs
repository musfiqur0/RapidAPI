namespace Domain.Models;

public class AppointmentLocalization : Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}
