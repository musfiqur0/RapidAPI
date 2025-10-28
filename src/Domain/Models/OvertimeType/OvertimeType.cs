namespace Domain.Models;

public class OvertimeType : Base
{
    public string Description { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
