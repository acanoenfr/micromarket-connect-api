namespace Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

public record Name
{
    public string Value;
    private Name(string value) => Value = value;
    public static Name Hydrate(string value) => new(value);
    public static Name Empty => new(string.Empty);
}
