namespace Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

public record CreatedAt
{
    public DateTimeOffset Value;
    private CreatedAt(DateTimeOffset value) => Value = value;
    public static CreatedAt Hydrate(DateTimeOffset value) => new(value);
    public static CreatedAt Empty => new(DateTimeOffset.UtcNow);
}
