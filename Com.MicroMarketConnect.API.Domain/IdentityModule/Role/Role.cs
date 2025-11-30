using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Role;

public record Role(
    Name Name,
    DisplayName DisplayName,
    Description Description)
{
    public static Role Hydrate(
        Name Name,
        DisplayName DisplayName,
        Description Description)
        => new(Name, DisplayName, Description);
}
