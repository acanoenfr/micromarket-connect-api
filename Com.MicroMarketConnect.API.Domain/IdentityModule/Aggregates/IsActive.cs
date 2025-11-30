namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record IsActive
{
    public bool Value;
    private IsActive(bool value) => Value = value;
    public static IsActive Hydrate(bool value) => new(value);
    public static IsActive Empty => new(true);
}
