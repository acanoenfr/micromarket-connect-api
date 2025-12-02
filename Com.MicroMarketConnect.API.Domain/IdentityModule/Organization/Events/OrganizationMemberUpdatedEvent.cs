using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationMemberUpdatedEvent(
    RowId OrganizationId,
    RowId UserId,
    MemberRole Role)
{
    public T Accept<T>(IOrganizationMemberEventVisitor<T> visitor) => visitor.Handle(this);
}
