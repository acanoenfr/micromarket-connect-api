using Com.MicroMarketConnect.API.Infrastructure.Database;

namespace Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static ModelBuilder ConfigureDefaultSchema(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mmc");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MicroMarketConnectDbContext).Assembly);

        return modelBuilder;
    }
}
