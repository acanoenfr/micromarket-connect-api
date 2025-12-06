using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationMemberDeletedEvent(
    RowId OrganizationId,
    RowId UserId) : IOrganizationMemberEvent
{
    public T Accept<T>(IOrganizationMemberEventVisitor<T> visitor) => visitor.Handle(this);
}
