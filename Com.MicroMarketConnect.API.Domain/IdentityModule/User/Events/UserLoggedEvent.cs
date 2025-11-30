using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;

public record UserLoggedEvent(
    Token Token) : IDomainEvent
{
}
