using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Com.MicroMarketConnect.API.Infrastructure.Providers;

public class TokenProvider : ITokenProvider
{
    private readonly JwtOptions _options;
    private readonly IDateProvider _dateProvider;

    public TokenProvider(
        IOptions<JwtOptions> options,
        IDateProvider dateProvider)
    {
        _options = options.Value;
        _dateProvider = dateProvider;
    }

    public string GenerateJwtToken(Guid id, string email, string[] roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.PreferredUsername, email)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresIn = _dateProvider.NewDate().Add(_options.ExpiresIn);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expiresIn.DateTime,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
