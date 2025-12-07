using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

public record DeleteOrganizationMemberCommand(
    Guid OrganizationId,
    Guid UserId) : IEventDrivenCommand;

public class DeleteOrganizationMemberCommandHandler(
    IOrganizationCommandRepository repository,
    IUserProvider userProvider) : CommandHandler<DeleteOrganizationMemberCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(DeleteOrganizationMemberCommand command)
    {
        var isGrantAccess = await repository.IsGrantAccess(command.OrganizationId, OrganizationRoleClaims.Owner, userProvider.GetId());
        if (isGrantAccess is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors.FirstOrDefault());
        if (!isGrantAccess.Value)
            return Result.Fail(Errors.Forbidden);

        var deleteMember = OrganizationMember.Delete(
            RowId.Hydrate(command.OrganizationId),
            RowId.Hydrate(command.UserId));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. deleteMember]);
    }
}
