using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;
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

    public static IEnumerable<IDomainEvent> Create(
        RowId OrganizationId,
        RowId UserId,
        RoleName Role)
    {
        yield return new OrganizationMemberAddedEvent(OrganizationId, UserId, Role);
    }

    public static IEnumerable<IDomainEvent> Update(
        RowId OrganizationId,
        RowId UserId,
        RoleName Role)
    {
        yield return new OrganizationMemberUpdatedEvent(OrganizationId, UserId, Role);
    }

    public static IEnumerable<IDomainEvent> Delete(
        RowId OrganizationId,
        RowId UserId)
    {
        yield return new OrganizationMemberDeletedEvent(OrganizationId, UserId);
    }
}
