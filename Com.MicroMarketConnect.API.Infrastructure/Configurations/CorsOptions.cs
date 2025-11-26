namespace Com.MicroMarketConnect.API.Infrastructure.Configurations;

public class CorsOptions
{
    public static readonly string SectionName = "Cors";

    public string[] CorsOrigin { get; set; } = [];
}
