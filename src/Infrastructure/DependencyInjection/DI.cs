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
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPaymentModeService, PaymentModeService>();
            services.AddScoped<IJobPostService, JobPostService>();

        }
    }
}
