namespace Domain.Models;

public class LeavesApprovalLocalization : Base
{
    public int LeavesApprovalId { get; set; }
    public LeavesApproval LeavesApproval { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
}
