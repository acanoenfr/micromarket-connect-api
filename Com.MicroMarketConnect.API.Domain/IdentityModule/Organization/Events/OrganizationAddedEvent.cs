using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationAddedEvent(
    Name Name,
    DisplayName DisplayName,
    Description Description)
{
    public static OrganizationAddedEvent Hydrate(
        Name Name,
        DisplayName DisplayName,
        Description Description)
        => new(Name, DisplayName, Description);
}
