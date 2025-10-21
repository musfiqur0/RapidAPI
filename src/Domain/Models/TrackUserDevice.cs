namespace Domain.Models;

public class TrackUserDevice
{
    public string Browser { get; set; }
    public string Location { get; set; }
    public string IP { get; set; }
    public string OS { get; set; }
    public string MapURL { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
