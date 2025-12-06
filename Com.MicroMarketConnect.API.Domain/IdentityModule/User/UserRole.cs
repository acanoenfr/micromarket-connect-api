using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public record UserRole(
    RowId UserId,
    RoleName Role)
{
    public static UserRole Hydrate(
        RowId UserId,
        RoleName Role)
        => new(UserId, Role);

    public static IEnumerable<IDomainEvent> Create(
        RowId UserId,
        RoleName Role)
    {
        yield return new UserRoleAddedEvent(UserId, Role);
    }

    public static IEnumerable<IDomainEvent> Delete(
        RowId UserId,
        RoleName Role)
    {
        yield return new UserRoleDeletedEvent(UserId, Role);
    }
}
