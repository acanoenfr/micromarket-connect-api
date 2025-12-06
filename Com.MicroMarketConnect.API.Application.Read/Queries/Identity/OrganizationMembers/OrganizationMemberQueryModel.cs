namespace Com.MicroMarketConnect.API.Application.Read.Queries.Identity.OrganizationMembers;

public class OrganizationMemberQueryModel
{
    public Guid Id { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
