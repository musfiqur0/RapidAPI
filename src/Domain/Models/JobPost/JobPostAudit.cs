namespace Domain.Models;

public class JobPostAudit : Audit
{
    public int Id { get; set; }
    public int JobPostId { get; set; }
    public int CompanyId { get; set; }
    public string JobTitle { get; set; }
    public int JobCategoryId { get; set; }
    public int JobTypeId { get; set; }
    public int NoOfVacancies { get; set; }
    public DateTime ClosingDate { get; set; }
    public int GenderId { get; set; }
    public int MinimumExperienceId { get; set; }
    public bool Featured { get; set; } = false;
    public int StatusId { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public bool Active { get; set; }
}
