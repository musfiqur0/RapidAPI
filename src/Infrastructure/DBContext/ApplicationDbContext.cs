using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAudit> EmployeeAudits { get; set; }
        public DbSet<EmployeeLocalization> EmployeeLocalizations { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Status> Status { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupAudit> GroupAudits { get; set; }
        public DbSet<GroupLocalization> GroupLocalizations { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PaymentModeAudit> PaymentModeAudits { get; set; }
        public DbSet<PaymentModeLocalization> PaymentModeLocalizations { get; set; }

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
