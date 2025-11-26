namespace Com.MicroMarketConnect.API.Core.Ports;

public interface IUserProvider
{
    Guid GetId();
    string GetEmail();
    string[] GetRoles();
}
