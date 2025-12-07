using Com.MicroMarketConnect.API.Application.Read.Ports;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;

public record GetOrganizationQuery(string Name) : IQuery<Result<OrganizationQueryModel>>;

public class GetOrganizationQueryHandler(
    IOrganizationQueryRepository repository,
    IUserProvider userProvider) : QueryHandler<GetOrganizationQuery, Result<OrganizationQueryModel>>
{
    protected override async Task<Result<OrganizationQueryModel>> Handle(GetOrganizationQuery query)
    {
        var isGrantAccess = await repository.IsGrantAccess(query.Name, OrganizationRoleClaims.Member, userProvider.GetId());
        if (isGrantAccess is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors.FirstOrDefault());
        if (!isGrantAccess.Value)
            return Result.Fail(Errors.Forbidden);

        return await repository.Get(query.Name);
    }
}
