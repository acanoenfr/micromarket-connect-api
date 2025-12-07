namespace Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;

public class OrganizationQueryModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
    public required IReadOnlyCollection<OrganizationMember> Members { get; set; }
}
