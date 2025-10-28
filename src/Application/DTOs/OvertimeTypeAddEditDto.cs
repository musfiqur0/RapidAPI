namespace Application.DTOs;

public class OvertimeTypeAddEditDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }

}

