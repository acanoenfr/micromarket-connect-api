using Com.MicroMarketConnect.API.Infrastructure.Providers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace Com.MicroMarketConnect.API.Web.Middlewares;

public class UserMiddleware
{
    private readonly RequestDelegate _next;

    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserProvider provider)
    {
        try
        {
            var id = context.User.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Sub))?.Value;
            var username = context.User.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.PreferredUsername))?.Value;
            var email = context.User.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Email))?.Value;
            var roles = context.User.Claims
                .Where(claim => claim.Type.Equals(ClaimTypes.Role))
                .Select(x => x.Value)
                .ToArray();

            provider.SetId(id);
            provider.SetEmail(email);
            provider.SetRoles(roles);
        }
        catch (AuthenticationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new AuthenticationException($"An error occured during authentication process: {ex.Message}");
        }

        await _next(context);
    }
}
