using Com.MicroMarketConnect.API.Web.HealthChecks.Definitions;
using Com.MicroMarketConnect.API.Web.HealthChecks.ResponseFormatters;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Com.MicroMarketConnect.API.Web.HealthChecks;

public class HealthCheckOptionsFactory
{
    internal static HealthCheckOptions Create(HealthCheckDefinition definition) =>
        new()
        {
            AllowCachingResponses = false,
            Predicate = definition.Predicate,
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var response = definition.Formatter.FormatType switch
                {
                    HealthCheckFormatType.Long => GenerateLongContent(definition, report),
                    HealthCheckFormatType.Short => GenerateShortContent(definition, report),
                    _ => GenerateShortContent(definition, report)
                };
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        response,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                        }));
            }
        };

    private static HealthCheckResponse GenerateLongContent(HealthCheckDefinition definition, HealthReport report)
    {
        return new HealthCheckResponse
        (
            definition.Formatter.StatusFormatter(report.Status).ToString(),
            report.Entries.ToDictionary(
                entryPair => entryPair.Key,
                entryPair =>
                    new IndividualHealthCheckResponse(
                        definition.Formatter.StatusFormatter(entryPair.Value.Status).ToString(),
                        entryPair.Value.Description ?? string.Empty,
                        entryPair.Value.Data.ToDictionary(
                            pair => pair.Key,
                            pair => pair.Value.ToString() ?? string.Empty),
                        entryPair.Value.Duration)),
            report.TotalDuration
        );
    }

    private static HealthCheckResponse GenerateShortContent(HealthCheckDefinition definition, HealthReport report)
    {
        return new HealthCheckResponse
        (
            definition.Formatter.StatusFormatter(report.Status).ToString(),
            new Dictionary<string, IndividualHealthCheckResponse>().AsReadOnly(),
            report.TotalDuration
        );
    }
}
