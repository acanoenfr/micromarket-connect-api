using Microsoft.AspNetCore.Authentication;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

internal class TestAuthHandlerOptions : AuthenticationSchemeOptions
{
    public string DefaultUserId { get; set; } = null!;
}
