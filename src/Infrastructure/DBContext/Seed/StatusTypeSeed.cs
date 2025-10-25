using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext.Seed;


public static class StatusTypeSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.Status.AnyAsync(cancellationToken))
        {
            var seedData = new List<StatusType>
            {
                new StatusType { Name = "General" },
                new StatusType { Name = "Workflow" },
                new StatusType { Name = "Pending" },
                new StatusType { Name = "Approved" },
                new StatusType { Name = "Rejected" },
                new StatusType { Name = "Completed" },
                new StatusType { Name = "Cancelled" }
            };

            //foreach (var item in seedData)
            //{
            //    // Check if it already exists by Name or Id
            //    var exists = await context.StatusTypes.AnyAsync(x => x.Name == item.Name, cancellationToken);
            //    if (!exists)
            //    {
            //        context.StatusTypes.Add(item);
            //    }
            //}
            context.StatusTypes.AddRange(seedData);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}