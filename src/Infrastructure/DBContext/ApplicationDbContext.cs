using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<OvertimeTypeAudit> OvertimeTypeAudits { get; set; }
        public DbSet<OvertimeTypeLocalization> OvertimeTypeLocalizations { get; set; }

        public DbSet<SourceType> SourceTypes { get; set; }
        public DbSet<SourceTypeAudit> SourceTypeAudits { get; set; }
        public DbSet<SourceTypeLocalization> SourceTypeLocalizations { get; set; }

        public DbSet<AllowanceType> AllowanceTypes { get; set; }
        public DbSet<AllowanceTypeAudit> AllowanceTypeAudits { get; set; }
        public DbSet<AllowanceTypeLocalization> AllowanceTypeLocalizations { get; set; }

        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeaveApplicationAudit> LeaveApplicationAudits { get; set; }
        public DbSet<LeaveApplicationLocalization> LeaveApplicationLocalizations { get; set; }

        public DbSet<Bonus> Bonuss { get; set; }
        public DbSet<BonusAudit> BonusAudits { get; set; }
        public DbSet<BonusLocalization> BonusLocalizations { get; set; }

        public DbSet<Onboarding> Onboardings { get; set; }
        public DbSet<OnboardingAudit> OnboardingAudits { get; set; }
        public DbSet<OnboardingLocalization> OnboardingLocalizations { get; set; }

        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionAudit> DeductionAudits { get; set; }
        public DbSet<DeductionLocalization> DeductionLocalizations { get; set; }

        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<AllowanceAudit> AllowanceAudits { get; set; }
        public DbSet<AllowanceLocalization> AllowanceLocalizations { get; set; }

        public DbSet<BenefitPenalty> BenefitPenaltys { get; set; }
        public DbSet<BenefitPenaltyAudit> BenefitPenaltyAudits { get; set; }
        public DbSet<BenefitPenaltyLocalization> BenefitPenaltyLocalizations { get; set; }

        public DbSet<Termination> Terminations { get; set; }
        public DbSet<TerminationAudit> TerminationAudits { get; set; }
        public DbSet<TerminationLocalization> TerminationLocalizations { get; set; }

        public DbSet<CandidateSelection> CandidateSelections { get; set; }
        public DbSet<CandidateSelectionAudit> CandidateSelectionAudits { get; set; }
        public DbSet<CandidateSelectionLocalization> CandidateSelectionLocalizations { get; set; }

        public DbSet<Increment> Increments { get; set; }
        public DbSet<IncrementAudit> IncrementAudits { get; set; }
        public DbSet<IncrementLocalization> IncrementLocalizations { get; set; }

        public DbSet<PreAlert> PreAlerts { get; set; }
        public DbSet<PreAlertAudit> PreAlertAudits { get; set; }
        public DbSet<PreAlertLocalization> PreAlertLocalizations { get; set; }

        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobPostAudit> JobPostAudits { get; set; }
        public DbSet<JobPostLocalization> JobPostLocalizations { get; set; }

        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PaymentModeAudit> PaymentModeAudits { get; set; }
        public DbSet<PaymentModeLocalization> PaymentModeLocalizations { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupAudit> GroupAudits { get; set; }
        public DbSet<GroupLocalization> GroupLocalizations { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAudit> EmployeeAudits { get; set; }
        public DbSet<EmployeeLocalization> EmployeeLocalizations { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            });
        }
    }
}
