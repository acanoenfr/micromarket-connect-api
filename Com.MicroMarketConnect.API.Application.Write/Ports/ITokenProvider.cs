namespace Com.MicroMarketConnect.API.Application.Write.Ports;

public interface ITokenProvider
{
    string GenerateJwtToken(Guid id, string email, string[] roles);
}
