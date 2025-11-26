namespace Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;

public record HealthCheckResponse(
    string Status,
    IReadOnlyDictionary<string, IndividualHealthCheckResponse> Entries,
    TimeSpan TotalDuration);
