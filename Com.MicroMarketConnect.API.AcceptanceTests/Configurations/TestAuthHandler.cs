using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

internal class TestAuthHandler : AuthenticationHandler<TestAuthHandlerOptions>
{
    internal static readonly string TestingUserIdHeaderName = "X-Integration-Testing-UserId";
    internal static readonly string TestingRolesHeaderName = "X-Integration-Testing-Roles";

    public static string TestingHeaderName => "X-Integration-Testing";
    public static string TestingHeaderValue => "TestingEnabled";

    public static readonly string AuthenticationScheme = "Test";
    private readonly string _defaultUserId;

    public TestAuthHandler(
        IOptionsMonitor<TestAuthHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
        _defaultUserId = options.CurrentValue.DefaultUserId;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.TryGetValue(TestingHeaderName, out var testingHeader) ||
            testingHeader != TestingHeaderValue)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "Test user"),
            Context.Request.Headers.TryGetValue(TestingUserIdHeaderName, out var userId)
                ? new Claim(ClaimTypes.NameIdentifier, userId[0] ?? "TestUserId")
                : new Claim(ClaimTypes.NameIdentifier, _defaultUserId)
        };

        if (Context.Request.Headers.TryGetValue(TestingRolesHeaderName, out var roles))
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role ?? string.Empty)));
        }

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
