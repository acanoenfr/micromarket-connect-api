using Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public class OrganizationMemberEntity
{
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = null!;

    public OrganizationEntity Organization { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
}
