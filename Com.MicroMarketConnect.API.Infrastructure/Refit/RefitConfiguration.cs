using Refit;
using System.Text.Json;

namespace Com.MicroMarketConnect.API.Infrastructure.Refit;

public class RefitConfiguration
{
    public static JsonSerializerOptions JsonSerializerOptions { get; } =
        SystemTextJsonContentSerializer.GetDefaultJsonSerializerOptions();
    public static RefitSettings Settings { get; } = new(
        new SystemTextJsonContentSerializer(JsonSerializerOptions));
}
