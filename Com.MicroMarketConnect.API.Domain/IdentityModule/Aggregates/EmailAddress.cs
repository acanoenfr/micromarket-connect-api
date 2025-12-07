namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record EmailAddress
{
    public string Value;
    private EmailAddress(string value) => Value = value;
    public static EmailAddress Hydrate(string value) => new(value);
    public static EmailAddress Empty => new(string.Empty);
}
