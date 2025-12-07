namespace Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;

public class OrganizationMember
{
    public Guid Id { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
