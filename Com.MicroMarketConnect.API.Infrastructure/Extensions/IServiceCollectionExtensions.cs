using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Infrastructure.Configurations;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Com.MicroMarketConnect.API.Infrastructure.Metrics;
using Com.MicroMarketConnect.API.Infrastructure.Orchestration;
using Com.MicroMarketConnect.API.Infrastructure.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration cfg)
    {
        services
            .AddOptions<JwtOptions>()
            .Bind(cfg.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<HasherOptions>()
            .Bind(cfg.GetSection(HasherOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services
            .AddScoped<UserProvider>()
            .AddScoped<IUserProvider>(provider => provider.GetRequiredService<UserProvider>());

        return services
            .AddScoped<IGuidProvider, GuidProvider>()
            .AddScoped<IDateProvider, DateProvider>()
            .AddScoped<ITokenProvider, TokenProvider>()
            .AddScoped<IHasherProvider, HasherProvider>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<AppMetrics>();
    }

    public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<MicroMarketConnectDbContext>(options =>
        {
            var connection = new SqlConnection(cfg.GetConnectionString("MicroMarketConnect"));
            options.UseSqlServer(connection, sqlOptions => sqlOptions.CommandTimeout(30));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IDomainEventsRepository, DomainEventsRepository>();
    }

    public static IServiceCollection AddOrchestration(this IServiceCollection services)
    {
        return services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(
                    Assembly.GetCallingAssembly(),
                    Assembly.GetAssembly(typeof(Com.MicroMarketConnect.API.Application.Read.AssemblyEntry))!,
                    Assembly.GetAssembly(typeof(Com.MicroMarketConnect.API.Application.Write.AssemblyEntry))!);
            })
            .AddTransient<IDispatcher, Dispatcher>()
            .AddScoped<WebDispatcher>()
            .AddTransient<Dispatcher>();
    }
}
