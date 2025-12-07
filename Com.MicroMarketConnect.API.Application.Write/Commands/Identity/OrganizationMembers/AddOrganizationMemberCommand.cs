using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

public record AddOrganizationMemberCommand(
    Guid OrganizationId,
    Guid UserId,
    string RoleName) : IEventDrivenCommand;

public class AddOrganizationMemberCommandHandler(
    IOrganizationCommandRepository repository,
    IUserProvider userProvider) : CommandHandler<AddOrganizationMemberCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(AddOrganizationMemberCommand command)
    {

        var isGrantAccess = await repository.IsGrantAccess(command.OrganizationId, OrganizationRoleClaims.Owner, userProvider.GetId());
        if (isGrantAccess is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors.FirstOrDefault());
        if (!isGrantAccess.Value)
            return Result.Fail(Errors.Forbidden);

        var addedMember = OrganizationMember.Create(
            RowId.Hydrate(command.OrganizationId),
            RowId.Hydrate(command.UserId),
            RoleName.Hydrate(command.RoleName));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. addedMember]);
    }
}
