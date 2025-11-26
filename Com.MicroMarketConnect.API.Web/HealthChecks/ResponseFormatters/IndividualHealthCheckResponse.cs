namespace Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;

public record IndividualHealthCheckResponse(
    string Status,
    string Description,
    IReadOnlyDictionary<string, string> Data,
    TimeSpan Duration);
