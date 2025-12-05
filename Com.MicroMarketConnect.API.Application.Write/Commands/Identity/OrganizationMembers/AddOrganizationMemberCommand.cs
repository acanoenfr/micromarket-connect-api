using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

public record AddOrganizationMemberCommand(
    Guid OrganizationId,
    Guid UserId,
    string RoleName) : IEventDrivenCommand;

public class AddOrganizationMemberCommandHandler : CommandHandler<AddOrganizationMemberCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(AddOrganizationMemberCommand command)
    {
        var addedMember = OrganizationMember.Create(
            RowId.Hydrate(command.OrganizationId),
            RowId.Hydrate(command.UserId),
            RoleName.Hydrate(command.RoleName));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. addedMember]);
    }
}
