using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;

public record UserRoleDeletedEvent(
    RowId UserId,
    RoleName RoleName) : IUserRoleEvent
{
    public T Accept<T>(IUserRoleEventVisitor<T> visitor) => visitor.Handle(this);
}
