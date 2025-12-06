namespace Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

public record DisplayName
{
    public string Value;
    private DisplayName(string value) => Value = value;
    public static DisplayName Hydrate(string value) => new(value);
    public static DisplayName Empty => new(string.Empty);
}
