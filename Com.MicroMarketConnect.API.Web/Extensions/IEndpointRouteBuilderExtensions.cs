using Com.MicroMarketConnect.API.Web.HealthChecks;
using Com.MicroMarketConnect.API.Web.HealthChecks.Definitions;

namespace Microsoft.AspNetCore.Routing;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapHealthChecks(this IEndpointRouteBuilder builder, HealthCheckDefinition definition)
    {
        return builder.MapHealthChecks(definition.Url, HealthCheckOptionsFactory.Create(definition));
    }
}
