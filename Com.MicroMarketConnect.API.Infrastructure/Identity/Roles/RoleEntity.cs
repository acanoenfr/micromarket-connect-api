using Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Roles;

public class RoleEntity
{
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<UserRoleEntity> UserRoles { get; set; } = [];
}
