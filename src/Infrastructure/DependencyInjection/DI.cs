using Application.Interfaces;
using Infrastructure.AutoMapper;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class DI
    {
        public static void DiExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IDeductionService, DeductionService>();
            services.AddScoped<IAllowanceService, AllowanceService>();
            services.AddScoped<IBenefitPenaltyService, BenefitPenaltyService>();
            services.AddScoped<ITerminationService, TerminationService>();
            services.AddScoped<ICandidateSelectionService, CandidateSelectionService>();
            services.AddScoped<IIncrementService, IncrementService>();
            services.AddScoped<IPreAlertService, PreAlertService>();
            services.AddScoped<IJobPostService, JobPostService>();
            services.AddScoped<IPaymentModeService, PaymentModeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

        }
    }
}
