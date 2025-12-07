using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Ports;

public interface IUserCommandRepository
{
    Task<Result<bool>> Exists(string email);
    Task<Result<string>> Login(string username, string password);
}
