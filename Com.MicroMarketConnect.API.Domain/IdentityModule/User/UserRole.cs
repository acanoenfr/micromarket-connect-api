using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public record UserRole(
    RowId UserId,
    Name RoleName)
{
    public static UserRole Hydrate(
        RowId UserId,
        Name RoleName)
        => new(UserId, RoleName);
}
