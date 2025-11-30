using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationUpdatedEvent(
    RowId Id,
    Name Name,
    DisplayName DisplayName,
    Description Description)
{
    public static OrganizationUpdatedEvent Hydrate(
        RowId Id,
        Name Name,
        DisplayName DisplayName,
        Description Description)
        => new(Id, Name, DisplayName, Description);
}
