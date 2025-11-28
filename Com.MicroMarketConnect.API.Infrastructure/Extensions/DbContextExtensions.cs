using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Com.MicroMarketConnect.API.Infrastructure.Extensions;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Roles;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

namespace Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static ModelBuilder ConfigureDefaultSchema(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mmc");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MicroMarketConnectDbContext).Assembly);

        return modelBuilder;
    }

    #region Identity module

    public static ModelBuilder ConfigureUsers(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.DiplayName).IsRequired();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
            entity.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(500);
            entity.Property(u => u.IsActive).IsRequired().HasDefaultValue(true);
            entity.Property(u => u.CreatedAt).IsRequired();

            entity.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.OrganizationMemberships).WithOne(m => m.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

            entity.ToTable("Users");
        });

        modelBuilder.Entity<UserRoleEntity>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleName });

            entity.Property(ur => ur.RoleName).IsRequired().HasMaxLength(100);

            entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            entity.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleName);

            entity.ToTable("UserRoles");
        });

        return modelBuilder;
    }

    public static ModelBuilder ConfigureRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(r => r.Name);

            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.DisplayName).IsRequired();
            entity.Property(r => r.Description).HasMaxLength(500);

            entity.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleName).OnDelete(DeleteBehavior.Cascade);

            entity.ToTable("Roles");

            entity.HasData(
                new RoleEntity
                {
                    Name = UserRole.PlatformUser.GetValue(),
                    DisplayName = "Platform user",
                    Description = "Simple access to application."
                },
                new RoleEntity
                {
                    Name = UserRole.PlatformModerator.GetValue(),
                    DisplayName = "Platform moderator",
                    Description = "Moderating users and organizations behaviors on the platform."
                },
                new RoleEntity
                {
                    Name = UserRole.PlatformAdmin.GetValue(),
                    DisplayName = "Platform administrator",
                    Description = "Managing the platform."
                });
        });

        return modelBuilder;
    }

    public static ModelBuilder ConfigureOrganizations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrganizationEntity>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.HasIndex(o => o.Name).IsUnique();

            entity.Property(o => o.Name).IsRequired().HasMaxLength(100);
            entity.Property(o => o.DisplayName).IsRequired();
            entity.Property(r => r.Description).HasMaxLength(500);
            entity.Property(o => o.CreatedAt).IsRequired();

            entity.HasMany(o => o.Members).WithOne(m => m.Organization).HasForeignKey(m => m.OrganizationId).OnDelete(DeleteBehavior.Cascade);

            entity.ToTable("Organizations");
        });

        modelBuilder.Entity<OrganizationMemberEntity>(entity =>
        {
            entity.HasKey(m => new { m.OrganizationId, m.UserId });

            entity.Property(m => m.Role).IsRequired().HasMaxLength(100);

            entity.HasOne(m => m.Organization).WithMany(o => o.Members).HasForeignKey(m => m.OrganizationId);
            entity.HasOne(m => m.User).WithMany(u => u.OrganizationMemberships).HasForeignKey(m => m.UserId);

            entity.ToTable("OrganizationMembers");
        });

        return modelBuilder;
    }

    #endregion
}
