using Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;

namespace Com.MicroMarketConnect.API.Web.HealthChecks.Definitions;

public class LivenessHealthCheckDefinition(string url = "/health") : HealthCheckDefinition(
    url,
    registration => registration.Tags.Contains("liveness"),
    new() { FormatType = HealthCheckFormatType.Short }
)
{
}
