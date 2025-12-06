using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Read.Ports;

public interface IOrganizationQueryRepository
{
    Task<Result<OrganizationQueryModel>> Get(string name);
}
