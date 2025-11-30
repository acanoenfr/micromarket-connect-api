using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;

public record OrganizationMemberAddedEvent(
    RowId OrganizationId,
    RowId UserId,
    MemberRole Role)
{
}
