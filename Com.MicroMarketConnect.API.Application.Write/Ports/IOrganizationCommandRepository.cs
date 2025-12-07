using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Ports;

public interface IOrganizationCommandRepository
{
    Task<Result<bool>> Exists(string name);
    Task<Result<bool>> IsGrantAccess(Guid id, string role, Guid userId);
}
