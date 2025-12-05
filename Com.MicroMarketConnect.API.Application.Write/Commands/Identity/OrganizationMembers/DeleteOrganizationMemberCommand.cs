using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

public record DeleteOrganizationMemberCommand(
    Guid OrganizationId,
    Guid UserId) : IEventDrivenCommand;

public class DeleteOrganizationMemberCommandHandler : CommandHandler<DeleteOrganizationMemberCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(DeleteOrganizationMemberCommand command)
    {
        var deleteMember = OrganizationMember.Delete(
            RowId.Hydrate(command.OrganizationId),
            RowId.Hydrate(command.UserId));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. deleteMember]);
    }
}
