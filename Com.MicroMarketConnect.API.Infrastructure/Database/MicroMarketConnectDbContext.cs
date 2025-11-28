using Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Roles;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Users;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Com.MicroMarketConnect.API.Infrastructure.Database;

public class MicroMarketConnectDbContext : DbContext
{
    public MicroMarketConnectDbContext(DbContextOptions<MicroMarketConnectDbContext> options): base(options)
    {
    }

    #region Identity Module
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    public DbSet<OrganizationMemberEntity> OrganizationMembers { get; set; }
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ConfigureDefaultSchema()
            .ConfigureUsers()
            .ConfigureRoles()
            .ConfigureOrganizations();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<string>()
            .HaveMaxLength(250);
    }
}
