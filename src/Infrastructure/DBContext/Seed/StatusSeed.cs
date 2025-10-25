using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext.Seed;


public static class StatusSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Status.AnyAsync(cancellationToken))
        {
            var generalType = await context.StatusTypes.FirstOrDefaultAsync(x => x.Name == "General", cancellationToken);
            var workflowType = await context.StatusTypes.FirstOrDefaultAsync(x => x.Name == "Workflow", cancellationToken);
            await context.Status.AddRangeAsync(new[]
            {
                // General statuses
                new Status { Name = "Active", Description = "Record is active", StatusTypeId = generalType.Id },
                new Status { Name = "Inactive", Description = "Record is inactive", StatusTypeId = generalType.Id },

                // Workflow statuses
                new Status { Name = "On Hold", Description = "Process is temporarily paused", StatusTypeId = workflowType.Id },
                new Status { Name = "Under Review", Description = "Record is currently under review", StatusTypeId = workflowType.Id },
                new Status { Name = "Pending", Description = "Awaiting approval or action", StatusTypeId = workflowType.Id },
                new Status { Name = "Approved", Description = "Record has been approved", StatusTypeId = workflowType.Id },
                new Status { Name = "Closed", Description = "Process or record has been closed", StatusTypeId = workflowType.Id },
                new Status { Name = "Expired", Description = "Record or process has expired", StatusTypeId = workflowType.Id }
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}