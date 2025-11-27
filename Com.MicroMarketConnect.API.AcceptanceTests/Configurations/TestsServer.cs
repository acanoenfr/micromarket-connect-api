using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;
using Com.MicroMarketConnect.API.Infrastructure.Configurations;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reqnroll;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

internal class TestsServer : IDisposable
{
    private readonly ScenarioContext _context;
    private readonly IHost _host;
    private bool _isDisposed;

    public IServiceProvider ServiceProvider => _host.Services;

    public TestsServer(ScenarioContext context)
    {
        _context = context;
        _host = Initialize();
    }

    private IHost Initialize()
    {
        var hostBuilder = Host
            .CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseEnvironment("acceptancetest")
                    .UseStartup<TestStartup>()
                    .ConfigureServices((ctx, services) =>
                    {
                        var jwtSection = ctx.Configuration.GetSection(JwtOptions.SectionName);
                        services
                            .AddOptions<JwtOptions>()
                            .Bind(jwtSection.GetSection(JwtOptions.SectionName))
                            .ValidateDataAnnotations()
                            .ValidateOnStart();

                        ConfigureAcceptanceTests(services);
                    })
                    .ConfigureTestServices(services =>
                    {
                        services
                            .AddSpyLoggers()
                            .AddSpyGuidProvider()
                            .AddSpyDateProvider()
                            .AddSpyUserProvider()
                            .AddSpyTokenProvider()
                            .AddTestAuthentication("TestUserId");
                    })
                    .UseTestServer();
            });

        var host = hostBuilder.Start();

        return host;
    }

    private void ConfigureAcceptanceTests(IServiceCollection services)
    {
        var databaseName = Guid.NewGuid().ToString();

        services
            .AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<MicroMarketConnectDbContext>(
                (provider, optionsBuilder) =>
                {
                    optionsBuilder.UseInMemoryDatabase(databaseName);
                    optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    optionsBuilder.UseInternalServiceProvider(provider);
                })
            .AddScoped<ITestsClient>(p => new AcceptanceClient(CreateApiClient(p), _context));
    }

    private static HttpClient CreateApiClient(IServiceProvider p)
    {
        return ((TestServer)p.GetRequiredService<IServer>()).CreateClient();
    }

    public void ForceStop()
    {
        CancellationTokenSource ctsSource = new CancellationTokenSource();
        ctsSource.Cancel();
        _host.StopAsync(ctsSource.Token).GetAwaiter().GetResult();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && !_isDisposed)
        {
            _host.Dispose();
            _isDisposed = true;
        }
    }
}
