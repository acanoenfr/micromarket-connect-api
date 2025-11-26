using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Com.MicroMarketConnect.API.Infrastructure.Database;

public class MicroMarketConnectDbContext : DbContext
{
    public MicroMarketConnectDbContext(DbContextOptions<MicroMarketConnectDbContext> options): base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ConfigureDefaultSchema();
    }
}
