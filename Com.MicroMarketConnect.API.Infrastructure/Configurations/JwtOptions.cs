namespace Com.MicroMarketConnect.API.Infrastructure.Configurations;

public class JwtOptions
{
    public static readonly string SectionName = "Jwt";

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public TimeSpan ExpiresIn { get; set; } = TimeSpan.Parse("7.00:00:00");
}
