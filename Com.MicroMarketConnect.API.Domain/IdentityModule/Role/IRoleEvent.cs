using Com.MicroMarketConnect.API.Core;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Role;

public interface IRoleEvent : IDomainEvent
{
    T Accept<T>(IRoleEventVisitor<T> visitor);
}
