using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

public record DeleteOrganizationCommand(Guid Id) : IEventDrivenCommand;

public class DeleteOrganizationCommandHandler : CommandHandler<DeleteOrganizationCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(DeleteOrganizationCommand command)
    {
        var deletedOrganization = Organization.Delete(
            RowId.Hydrate(command.Id));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. deletedOrganization]);
    }
}
