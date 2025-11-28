using Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Roles;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Users;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity;

public interface IIdentityDbContext
{
    DbSet<UserEntity> Users { get; }
    DbSet<UserRoleEntity> UserRoles { get; }
    DbSet<RoleEntity> Roles { get; }
    DbSet<OrganizationEntity> Organizations { get; }
    DbSet<OrganizationMemberEntity> OrganizationMembers { get; }
}
