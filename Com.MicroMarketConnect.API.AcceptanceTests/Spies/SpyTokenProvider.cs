using Com.MicroMarketConnect.API.Application.Write.Ports;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Spies;

internal class SpyTokenProvider : ITokenProvider
{
    public string GenerateJwtToken(Guid id, string email, string[] roles)
    {
        return $"{id.ToString()}_{email}_{string.Join(",", roles)}";
    }
}
