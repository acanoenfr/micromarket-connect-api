using Com.MicroMarketConnect.API.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args)
            .ConfigureHostConfiguration(config => config.AddEnvironmentVariables())
            .Build();

        Result.Setup(cfg =>
        {
            cfg.Logger = new ResultLogger(host.Services.GetRequiredService<ILogger<ResultLogger>>());
        });

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MicroMarketConnectDbContext>();
        db.Database.Migrate();

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
