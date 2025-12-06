using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;

public record UserAddedEvent(
    RowId Id,
    DisplayName DisplayName,
    EmailAddress Email,
    PasswordHash PasswordHash,
    PasswordSalt PasswordSalt) : IUserEvent
{
    public T Accept<T>(IUserEventVisitor<T> visitor) => visitor.Handle(this);
}
