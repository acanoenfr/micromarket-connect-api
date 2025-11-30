namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;

public record MemberRole
{
    public string Value;
    private MemberRole(string value) => Value = value;
    public static MemberRole Hydrate(string value) => new(value);
    public static MemberRole Empty => new(string.Empty);
}
