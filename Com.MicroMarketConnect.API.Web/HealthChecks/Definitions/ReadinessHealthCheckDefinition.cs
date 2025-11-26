using Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;

namespace Com.MicroMarketConnect.API.Web.HealthChecks.Definitions;

public class ReadinessHealthCheckDefinition(string url = "/health/ready") : HealthCheckDefinition(
    url,
    registration => registration.Tags.Contains("readiness"),
    new() { FormatType = HealthCheckFormatType.Long }
)
{
}
