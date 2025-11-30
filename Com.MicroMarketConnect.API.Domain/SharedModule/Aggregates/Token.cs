namespace Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

public record Token
{
    public string Value;
    private Token(string value) => Value = value;
    public static Token Hydrate(string value) => new(value);
    public static Token Empty => new(string.Empty);
}
