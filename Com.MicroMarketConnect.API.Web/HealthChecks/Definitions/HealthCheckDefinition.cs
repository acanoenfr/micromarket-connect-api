using Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Com.MicroMarketConnect.API.Web.HealthChecks.Definitions;

public class HealthCheckDefinition(
    string url,
    Func<HealthCheckRegistration, bool> predicate,
    HealthChecksFormatter formatter)
{
    protected internal string Url { get; init; } = url;
    protected internal HealthChecksFormatter Formatter { get; init; } = formatter;
    protected internal Func<HealthCheckRegistration, bool> Predicate { get; init; } = predicate;
}
