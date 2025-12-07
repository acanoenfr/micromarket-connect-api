using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public interface IUserEventVisitor<out T>
{
    T Handle(UserAddedEvent @event);
}
