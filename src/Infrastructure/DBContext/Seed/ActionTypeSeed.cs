using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext.Seed;

public static class ActionTypeSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.ActionTypes.AnyAsync(cancellationToken))
        {
            var actionTypes = new List<ActionType>
            {
                new() { Name = "Create Action", Description = "CREATE - Active" },
                new() { Name = "Update Action", Description = "UPDATE - Active" },
                new() { Name = "Delete Action", Description = "DELETE - Active" },
                new() { Name = "View Action", Description = "VIEW - Inactive" },
                new() { Name = "Approve Action", Description = "APPROVE - Active" },
                new() { Name = "Reject Action", Description = "REJECT - Draft" },
                new() { Name = "Submit Action", Description = "SUBMIT - Active" },
                new() { Name = "Cancel Action", Description = "CANCEL - Active" },
                new() { Name = "Archive Action", Description = "ARCHIVE - Inactive" },
                new() { Name = "Restore Action", Description = "RESTORE - Active" },
                new() { Name = "Action Type 11", Description = "ACT011 - Inactive" },
                new() { Name = "Action Type 12", Description = "ACT012 - Inactive" },
                new() { Name = "Action Type 13", Description = "ACT013 - Draft" },
                new() { Name = "Action Type 14", Description = "ACT014 - Inactive" },
                new() { Name = "Action Type 15", Description = "ACT015 - Inactive" },
                new() { Name = "Action Type 16", Description = "ACT016 - Draft" },
                new() { Name = "Action Type 17", Description = "ACT017 - Inactive" },
                new() { Name = "Action Type 18", Description = "ACT018 - Draft" },
                new() { Name = "Action Type 19", Description = "ACT019 - Active" },
                new() { Name = "Action Type 20", Description = "ACT020 - Inactive" },
                new() { Name = "Action Type 21", Description = "ACT021 - Inactive" },
                new() { Name = "Action Type 22", Description = "ACT022 - Inactive" },
                new() { Name = "Action Type 23", Description = "ACT023 - Inactive" },
                new() { Name = "Action Type 24", Description = "ACT024 - Inactive" },
                new() { Name = "Action Type 25", Description = "ACT025 - Inactive" },
                new() { Name = "Action Type 26", Description = "ACT026 - Inactive" },
                new() { Name = "Action Type 27", Description = "ACT027 - Inactive" },
                new() { Name = "Action Type 28", Description = "ACT028 - Active" },
                new() { Name = "Action Type 29", Description = "ACT029 - Draft" },
                new() { Name = "Action Type 30", Description = "ACT030 - Draft" },
                new() { Name = "Action Type 31", Description = "ACT031 - Inactive" },
                new() { Name = "Action Type 32", Description = "ACT032 - Draft" },
                new() { Name = "Action Type 33", Description = "ACT033 - Active" },
                new() { Name = "Action Type 34", Description = "ACT034 - Active" },
                new() { Name = "Action Type 35", Description = "ACT035 - Inactive" },
                new() { Name = "Action Type 36", Description = "ACT036 - Inactive" },
                new() { Name = "Action Type 37", Description = "ACT037 - Draft" },
                new() { Name = "Action Type 38", Description = "ACT038 - Active" },
                new() { Name = "Action Type 39", Description = "ACT039 - Draft" },
                new() { Name = "Action Type 40", Description = "ACT040 - Inactive" },
                new() { Name = "Action Type 41", Description = "ACT041 - Draft" },
                new() { Name = "Action Type 42", Description = "ACT042 - Active" },
                new() { Name = "Action Type 43", Description = "ACT043 - Active" },
                new() { Name = "Action Type 44", Description = "ACT044 - Draft" },
                new() { Name = "Action Type 45", Description = "ACT045 - Draft" },
                new() { Name = "Action Type 46", Description = "ACT046 - Draft" }
            };

            await context.ActionTypes.AddRangeAsync(actionTypes, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
