using Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

public class UserEntity
{
    public Guid Id { get; set; }
    public string DiplayName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }

    public ICollection<UserRoleEntity> UserRoles { get; set; } = [];
    public ICollection<OrganizationMemberEntity> OrganizationMemberships { get; set; } = [];
}
