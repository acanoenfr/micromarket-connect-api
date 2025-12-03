using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public record OrganizationMember(
    RowId OrganizationId,
    RowId UserId,
    RoleName Role)
{
    public static OrganizationMember Hydrate(
        RowId OrganizationId,
        RowId UserId,
        RoleName Role)
        => new(OrganizationId, UserId, Role);
}
