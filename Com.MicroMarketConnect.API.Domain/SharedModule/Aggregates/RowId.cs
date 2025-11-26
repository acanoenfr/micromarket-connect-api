namespace Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;

public record RowId
{
    public Guid Value;
    private RowId(Guid value) => Value = value;
    public static RowId Hydrate(Guid value) => new(value);
    public static RowId Empty => new(Guid.Empty);
}
