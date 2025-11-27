using Com.MicroMarketConnect.API.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

internal class TestStartup(IHostEnvironment environment, IConfiguration configuration) : Startup(environment, configuration)
{
    protected override void ConfigureAuthMiddleware(IApplicationBuilder app)
    {
        // Do not for integration testing
    }
}
