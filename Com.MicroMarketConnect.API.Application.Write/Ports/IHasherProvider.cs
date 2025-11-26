namespace Com.MicroMarketConnect.API.Application.Write.Ports;

public record PasswordHash(string Hash, string Salt);

public interface IHasherProvider
{
    PasswordHash Hash(string password);
    bool Verify(string password, PasswordHash hashedPassword);
}
