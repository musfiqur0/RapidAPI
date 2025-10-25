using Infrastructure.DBContext.Seed;

namespace Infrastructure.DBContext;

public static class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        await StatusTypeSeed.SeedAsync(context, cancellationToken);
        await StatusSeed.SeedAsync(context, cancellationToken);
        await LanguageSeed.SeedAsync(context, cancellationToken);
        await ActionTypeSeed.SeedAsync(context, cancellationToken);
    }
}
