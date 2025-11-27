using Com.MicroMarketConnect.API.AcceptanceTests.Configurations;
using Com.MicroMarketConnect.API.AcceptanceTests.Spies;
using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Web;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;

internal static class IServiceCollectionExtensions
{
    internal static IServiceCollection AddSpyLoggers(this IServiceCollection services)
    {
        return services.AddSingleton<ILogger<Startup>, SpyLogger<Startup>>();
    }

    internal static IServiceCollection AddSpyGuidProvider(this IServiceCollection services)
    {
        var spyGuidProvider = new SpyGuidProvider();
        return services
            .AddScoped(_ => spyGuidProvider)
            .AddScoped<IGuidProvider>(_ => spyGuidProvider);
    }

    internal static IServiceCollection AddSpyDateProvider(this IServiceCollection services)
    {
        var spyDateProvider = new SpyDateProvider();
        return services
            .AddScoped(_ => spyDateProvider)
            .AddScoped<IDateProvider>(_ => spyDateProvider);
    }

    internal static IServiceCollection AddSpyUserProvider(this IServiceCollection services)
    {
        var spyUserProvider = new SpyUserProvider();
        return services
            .AddScoped(_ => spyUserProvider)
            .AddScoped<IUserProvider>(_ => spyUserProvider);
    }

    internal static IServiceCollection AddSpyTokenProvider(this IServiceCollection services)
    {
        var spyTokenProvider = new SpyTokenProvider();
        return services
            .AddScoped(_ => spyTokenProvider)
            .AddScoped<ITokenProvider>(_ => spyTokenProvider);
    }

    internal static IServiceCollection AddTestAuthentication(this IServiceCollection services, string defaultUserId)
    {
        services
            .Configure<TestAuthHandlerOptions>(options => options.DefaultUserId = defaultUserId);

        services
            .AddAuthentication(TestAuthHandler.AuthenticationScheme)
            .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, _ => { });

        return services;
    }
}
