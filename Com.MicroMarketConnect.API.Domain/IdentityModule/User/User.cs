using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public record User(
    RowId Id,
    DisplayName DisplayName,
    EmailAddress Email,
    PasswordHash PasswordHash,
    PasswordSalt PasswordSalt,
    IsActive IsActive,
    CreatedAt CreatedAt,
    LastLoginAt LastLoginAt)
{
    public static User Hydrate(
        RowId Id,
        DisplayName DisplayName,
        EmailAddress Email,
        PasswordHash PasswordHash,
        PasswordSalt PasswordSalt,
        IsActive IsActive,
        CreatedAt CreatedAt,
        LastLoginAt LastLoginAt)
        => new(Id, DisplayName, Email, PasswordHash, PasswordSalt, IsActive, CreatedAt, LastLoginAt);

    public static IEnumerable<IDomainEvent> Create(
        RowId Id,
        DisplayName DisplayName,
        EmailAddress Email,
        PasswordHash PasswordHash,
        PasswordSalt PasswordSalt,
        IEnumerable<RoleName> RoleNames)
    {
        yield return new UserAddedEvent(Id, DisplayName, Email, PasswordHash, PasswordSalt);

        foreach (var RoleName in RoleNames)
            yield return new UserRoleAddedEvent(Id, RoleName);
    }
}
