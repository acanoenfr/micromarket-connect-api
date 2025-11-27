using Com.MicroMarketConnect.API.Core.Ports;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Spies;

internal class SpyGuidProvider(params Guid[] guids) : IGuidProvider
{
    public List<Guid> Guids = [.. guids];

    public Guid NewGuid()
    {
        if (Guids.Count == 0)
            throw new InvalidOperationException("Guid should be overriden");

        var guid = Guids[0];
        Guids.RemoveAt(0);
        return guid;
    }
}
