using Com.MicroMarketConnect.API.Infrastructure.Identity.Roles;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

public class UserRoleEntity
{
    public Guid UserId { get; set; }
    public string RoleName { get; set; } = null!;

    public UserEntity User { get; set; } = null!;
    public RoleEntity Role { get; set; } = null!;
}
