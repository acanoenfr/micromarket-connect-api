using Asp.Versioning;
using Com.MicroMarketConnect.API.Infrastructure.Configurations;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Com.MicroMarketConnect.API.Web.HealthChecks.Definitions;
using Com.MicroMarketConnect.API.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json;

namespace Com.MicroMarketConnect.API.Web;

public class Startup(
    IHostEnvironment environment,
    IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var corsOptions = configuration.GetSection(CorsOptions.SectionName).Get<CorsOptions>();
        services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(corsOptions?.CorsOrigin ?? []);
        }));

        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidAudience = jwtOptions?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions?.Secret ?? string.Empty))
                };
            });

        services
            .AddAuthorization();

        services
            .AddControllers()
            .AddApplicationPart(typeof(Startup).Assembly)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services
            .AddEndpointsApiExplorer();

        services
            .AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
                setup.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        services
            .AddConfigurations(configuration)
            .AddProviders()
            .AddRepositories()
            .AddOrchestration()
            .AddServices();

        services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>()
            .AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwagger>()
            .AddSwaggerGen();

        services
            .AddHealthChecks()
            .AddDbContextCheck<MicroMarketConnectDbContext>(
                name: "mmc-database",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["readiness"])
            .AddCheck(
                name: "ready",
                check: () => HealthCheckResult.Healthy(),
                tags: ["liveness"]);

        ConfigureSQLClients(services);
    }

    public void Configure(IApplicationBuilder app, IConfiguration config)
    {
        app.UseRouting();
        if (environment.IsDevelopment() || environment.IsEnvironment("Local"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        ConfigureAuthMiddleware(app);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks(new LivenessHealthCheckDefinition());
            endpoints.MapHealthChecks(new ReadinessHealthCheckDefinition());
        });
    }

    #region Protected methods

    protected virtual void ConfigureAuthMiddleware(IApplicationBuilder app)
        => app
            .UseMiddleware<UserMiddleware>();

    protected virtual void ConfigureSQLClients(IServiceCollection services)
        => services
            .AddSqlServerDbContext(configuration);

    #endregion
}
