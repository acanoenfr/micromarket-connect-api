using System.Diagnostics.Metrics;

namespace Com.MicroMarketConnect.API.Infrastructure.Metrics;

public class AppMetrics
{
    public static readonly string MetricsName = "AppMetrics";

    public AppMetrics(IMeterFactory meterFactory)
    {
        meterFactory.Create(MetricsName);
    }
}
