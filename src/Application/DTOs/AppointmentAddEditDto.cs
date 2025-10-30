namespace Application.DTOs;

public class AppointmentAddEditDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime AppointmentTime { get; set; }
    public int AppointedById { get; set; }
}
