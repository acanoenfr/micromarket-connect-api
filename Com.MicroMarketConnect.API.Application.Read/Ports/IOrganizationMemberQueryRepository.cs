using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.OrganizationMembers;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Read.Ports;

public interface IOrganizationMemberQueryRepository
{
    Task<Result<IReadOnlyCollection<OrganizationMemberQueryModel>>> GetAll(string name);
}
