namespace Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

public record Description
{
    public string? Value;
    private Description(string? value) => Value = value;
    public static Description Hydrate(string value) => new(value);
    public static Description Empty => new((string?)null);
    public bool HasValue => !string.IsNullOrWhiteSpace(Value);
}
