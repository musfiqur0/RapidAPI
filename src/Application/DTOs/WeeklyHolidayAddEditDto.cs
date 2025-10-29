namespace Application.DTOs;

public class WeeklyHolidayAddEditDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
}

  