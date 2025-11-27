using Com.MicroMarketConnect.API.Core.Ports;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Spies;

internal class SpyDateProvider(params DateTimeOffset[] dates) : IDateProvider
{
    public List<DateTimeOffset> Dates = [.. dates];

    public DateTimeOffset NewDate()
    {
        if (Dates.Count == 0)
            throw new InvalidOperationException("Date should be overriden");

        var dates = Dates[0];
        Dates.RemoveAt(0);
        return dates;
    }
}
