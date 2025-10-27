namespace Domain.Models;

public class OnboardingAudit : Audit
{
    public int Id { get; set; }
    public int OnboardingId { get; set; }
    public Onboarding Onboarding { get; set; }
    public int StaffId { get; set; }
    public int GeneralInformationId { get; set; }
    public string StaffFullName { get; set; }
    public string Address { get; set; }
    public string AssetAllocation { get; set; }
    public int TypeOfTrainingId { get; set; }
    public int TrainingProgramId { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }

}
