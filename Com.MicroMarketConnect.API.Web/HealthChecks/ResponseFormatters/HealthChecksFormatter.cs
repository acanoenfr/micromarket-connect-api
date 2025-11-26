using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;

public record HealthChecksFormatter
{
    public HealthCheckFormatType FormatType = HealthCheckFormatType.Short;
    public Func<HealthStatus, HealthStatus> StatusFormatter = status => status;
}
