using Com.MicroMarketConnect.API.Core.Ports;

namespace Com.MicroMarketConnect.API.Infrastructure.Providers;

public class DateProvider : IDateProvider
{
    public DateTimeOffset NewDate()
        => DateTimeOffset.UtcNow.ToOffset(TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time").BaseUtcOffset);
}
