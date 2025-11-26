using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Com.MicroMarketConnect.API.Infrastructure.Providers;

public class HasherProvider : IHasherProvider
{
    private readonly HasherOptions _options;

    public HasherProvider(IOptions<HasherOptions> options)
    {
        _options = options.Value;
    }

    public PasswordHash Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_options.SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Iterations, _options.Algorithm, _options.HashSize);

        return new(Hash: Convert.ToHexString(hash), Salt: Convert.ToHexString(salt));
    }

    public bool Verify(string password, PasswordHash hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(password) || hashedPassword is null)
            return false;

        byte[] hash = Convert.FromHexString(hashedPassword.Hash);
        byte[] salt = Convert.FromHexString(hashedPassword.Salt);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Iterations, _options.Algorithm, _options.HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
