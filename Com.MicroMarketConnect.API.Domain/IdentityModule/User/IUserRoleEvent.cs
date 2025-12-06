using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public interface IUserRoleEvent : IDomainEvent
{
    T Accept<T>(IUserRoleEventVisitor<T> visitor);
}
