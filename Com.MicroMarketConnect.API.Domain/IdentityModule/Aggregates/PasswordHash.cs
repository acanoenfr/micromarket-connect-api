namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record PasswordHash
{
    public string Value;
    public PasswordHash(string value) => Value = value;
    public static PasswordHash Hydrate(string value) => new(value);
    public static PasswordHash Empty => new(string.Empty);
}
