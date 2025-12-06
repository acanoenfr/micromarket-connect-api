using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

public record UpdateOrganizationCommand(
    Guid Id,
    string Name,
    string DisplayName,
    string Description) : IEventDrivenCommand;

public class UpdateOrganizationCommandHandler : CommandHandler<UpdateOrganizationCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(UpdateOrganizationCommand command)
    {
        var updatedOrganization = Organization.Update(
            RowId.Hydrate(command.Id),
            Name.Hydrate(command.Name),
            DisplayName.Hydrate(command.DisplayName),
            Description.Hydrate(command.Description));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. updatedOrganization]);
    }
}
