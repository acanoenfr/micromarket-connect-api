using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
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
}
