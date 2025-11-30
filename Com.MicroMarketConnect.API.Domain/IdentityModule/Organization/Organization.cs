using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;

public record Organization(
    RowId Id,
    Name Name,
    DisplayName DisplayName,
    Description Description,
    CreatedAt CreatedAt)
{
    public static Organization Hydrate(
        RowId Id,
        Name Name,
        DisplayName DisplayName,
        Description Description,
        CreatedAt CreatedAt)
        => new(Id, Name, DisplayName, Description, CreatedAt);
}
