using Com.MicroMarketConnect.API.Core.Ports;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Spies;

internal class SpyUserProvider : IUserProvider
{
    public Guid Id = Guid.Empty;
    public string Email = string.Empty;
    public string[] Roles = [];

    public Guid GetId() => Id;
    public string GetEmail() => Email;
    public string[] GetRoles() => Roles;
}
