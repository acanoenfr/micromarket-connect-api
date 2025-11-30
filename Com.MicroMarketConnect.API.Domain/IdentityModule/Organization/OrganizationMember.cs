using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public record OrganizationMember(
    RowId OrganizationId,
    RowId UserId,
    MemberRole Role)
{
    public static OrganizationMember Hydrate(
        RowId OrganizationId,
        RowId UserId,
        MemberRole Role)
        => new(OrganizationId, UserId, Role);
}
