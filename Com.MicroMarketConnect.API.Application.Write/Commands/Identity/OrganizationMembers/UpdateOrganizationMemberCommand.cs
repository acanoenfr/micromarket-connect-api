using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

public record UpdateOrganizationMemberCommand(
    Guid OrganizationId,
    Guid UserId,
    string RoleName) : IEventDrivenCommand;

public class UpdateOrganizationMemberCommandHandler : CommandHandler<UpdateOrganizationMemberCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(UpdateOrganizationMemberCommand command)
    {
        var updateMember = OrganizationMember.Update(
            RowId.Hydrate(command.OrganizationId),
            RowId.Hydrate(command.UserId),
            RoleName.Hydrate(command.RoleName));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. updateMember]);
    }
}
