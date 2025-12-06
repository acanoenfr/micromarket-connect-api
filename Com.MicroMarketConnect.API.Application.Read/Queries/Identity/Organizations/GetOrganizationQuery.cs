using Com.MicroMarketConnect.API.Application.Read.Ports;
using Com.MicroMarketConnect.API.Core.Orchestration;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;

public record GetOrganizationQuery(string Name) : IQuery<Result<OrganizationQueryModel>>;

public class GetOrganizationQueryHandler(
    IOrganizationQueryRepository repository) : QueryHandler<GetOrganizationQuery, Result<OrganizationQueryModel>>
{
    protected override Task<Result<OrganizationQueryModel>> Handle(GetOrganizationQuery query)
        => repository.Get(query.Name);
}
