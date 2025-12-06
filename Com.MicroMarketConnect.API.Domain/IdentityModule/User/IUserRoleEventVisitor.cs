using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public interface IUserRoleEventVisitor<out T>
{
    T Handle(UserRoleAddedEvent @event);
    T Handle(UserRoleDeletedEvent @event);
}
