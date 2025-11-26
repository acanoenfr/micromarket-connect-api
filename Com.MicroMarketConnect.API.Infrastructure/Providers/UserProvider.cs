using Com.MicroMarketConnect.API.Core.Ports;

namespace Com.MicroMarketConnect.API.Infrastructure.Providers;

public class UserProvider : IUserProvider
{
    private Guid _id = Guid.Empty;
    private string _email = string.Empty;
    private string[] _roles = [];

    public Guid GetId() => _id;
    public void SetId(string? id)
    {
        _id = !string.IsNullOrEmpty(id) ? Guid.Parse(id) : Guid.Empty;
    }

    public string GetEmail() => _email;
    public void SetEmail(string? email)
    {
        _email = email ?? string.Empty;
    }

    public string[] GetRoles() => _roles;
    public void SetRoles(string[]? roles)
    {
        _roles = roles ?? [];
    }
}
