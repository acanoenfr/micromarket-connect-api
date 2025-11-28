namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public class OrganizationEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<OrganizationMemberEntity> Members { get; set; } = [];
}
