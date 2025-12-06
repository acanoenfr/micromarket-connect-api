using Com.MicroMarketConnect.API.Application.Read.Ports;
using Com.MicroMarketConnect.API.Core.Orchestration;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Read.Queries.Identity.OrganizationMembers;

public record GetOrganizationMemberQuery(string Name) : IQuery<Result<IReadOnlyCollection<OrganizationMemberQueryModel>>>;

public class GetOrganizationMemberQueryHandler(
    IOrganizationMemberQueryRepository repository) : QueryHandler<GetOrganizationMemberQuery, Result<IReadOnlyCollection<OrganizationMemberQueryModel>>>
{
    protected override Task<Result<IReadOnlyCollection<OrganizationMemberQueryModel>>> Handle(GetOrganizationMemberQuery query)
        => repository.GetAll(query.Name);
}
