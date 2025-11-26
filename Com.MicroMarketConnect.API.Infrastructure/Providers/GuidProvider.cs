using Com.MicroMarketConnect.API.Core.Ports;

namespace Com.MicroMarketConnect.API.Infrastructure.Providers;

public class GuidProvider : IGuidProvider
{
    public Guid NewGuid()
        => Guid.NewGuid();
}
