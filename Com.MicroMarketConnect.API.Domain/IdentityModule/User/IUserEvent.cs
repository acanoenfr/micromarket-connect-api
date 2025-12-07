using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public interface IUserEvent : IDomainEvent
{
    T Accept<T>(IUserEventVisitor<T> visitor);
}
