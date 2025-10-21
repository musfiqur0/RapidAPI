namespace Domain.Models;

public class Audit : TrackUserDevice
{
    public short? ActionTypeId { get; set; }
    public long ActionUserId { get; set; }
    public DateTime ActionAt { get; set; } = DateTime.Now;
    public bool IsDefault { get; set; }
    public short? StatusTypeId { get; set; }
    public ActionType ActionType { get; set; }
    public StatusType StatusType { get; set; }
}
