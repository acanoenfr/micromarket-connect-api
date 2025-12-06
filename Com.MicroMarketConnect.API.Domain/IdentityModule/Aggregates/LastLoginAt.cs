namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record LastLoginAt
{
    public DateTimeOffset? Value;
    private LastLoginAt(DateTimeOffset? value) => Value = value;
    public static LastLoginAt Hydrate(DateTimeOffset value) => new(value);
    public static LastLoginAt Empty => new((DateTimeOffset?)null);
    public bool HasValue => Value.HasValue;
}
