using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationUpdatedEvent(
    RowId Id,
    Name Name,
    DisplayName DisplayName,
    Description Description) : IOrganizationEvent
{
    public T Accept<T>(IOrganizationEventVisitor<T> visitor) => visitor.Handle(this);
}
