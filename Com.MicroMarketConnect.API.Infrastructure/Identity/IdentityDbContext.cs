using Com.MicroMarketConnect.API.Infrastructure.Database;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Roles;
using Com.MicroMarketConnect.API.Infrastructure.Identity.Users;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity;

public class IdentityDbContext(MicroMarketConnectDbContext dbContext) : IIdentityDbContext
{
    public DbSet<UserEntity> Users => dbContext.Users;
    public DbSet<UserRoleEntity> UserRoles => dbContext.UserRoles;
    public DbSet<RoleEntity> Roles => dbContext.Roles;
    public DbSet<OrganizationEntity> Organizations => dbContext.Organizations;
    public DbSet<OrganizationMemberEntity> OrganizationMembers => dbContext.OrganizationMembers;
}
