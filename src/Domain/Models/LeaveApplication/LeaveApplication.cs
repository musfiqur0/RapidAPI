namespace Domain.Models;

public class LeaveApplication
{
    public int Id { get; set; }
    public int LeaveTypesId { get; set; }
    public string Notes { get; set; }
    public short StatusId { get; set; }
    public Status Status { get; set; }
    public bool Default { get; set; }
    // Generic attachments (filtered by EntityName + EntityId)
    //public ICollection<Attachment> Attachments { get; set; }
}

