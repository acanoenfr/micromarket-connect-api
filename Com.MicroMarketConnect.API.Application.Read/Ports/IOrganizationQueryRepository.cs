using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Read.Ports;

public interface IOrganizationQueryRepository
{
    Task<Result<OrganizationQueryModel>> Get(string name);
    Task<Result<bool>> IsGrantAccess(string name, string role, Guid userId);
}
