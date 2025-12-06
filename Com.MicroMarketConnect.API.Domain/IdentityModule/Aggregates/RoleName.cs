namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record RoleName
{
    public string Value;
    private RoleName(string value) => Value = value;
    public static RoleName Hydrate(string value) => new(value);
    public static RoleName Empty => new(string.Empty);
}
