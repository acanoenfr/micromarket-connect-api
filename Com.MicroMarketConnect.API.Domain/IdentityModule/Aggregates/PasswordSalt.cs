namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record PasswordSalt
{
    public string Value;
    public PasswordSalt(string value) => Value = value;
    public static PasswordSalt Hydrate(string value) => new(value);
    public static PasswordSalt Empty => new(string.Empty);
}
