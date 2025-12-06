using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Ports;

public interface IOrganizationCommandRepository
{
    Task<Result<bool>> Exists(string name);
    Task<Result<bool>> IsGrantAccess(string name, string role, Guid userId);
}
