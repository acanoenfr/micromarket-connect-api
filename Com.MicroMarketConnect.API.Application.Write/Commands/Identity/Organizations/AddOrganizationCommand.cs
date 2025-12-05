using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

public record AddOrganizationCommand(
    string Name,
    string DisplayName,
    string Description) : IEventDrivenCommand;

public class AddOrganizationCommandHandler(
    IUserProvider userProvider,
    IGuidProvider guidProvider) : CommandHandler<AddOrganizationCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(AddOrganizationCommand command)
    {
        var id = RowId.Hydrate(guidProvider.NewGuid());
        var userId = RowId.Hydrate(userProvider.GetId());

        var addedOrganization = Organization.Create(
            id,
            Name.Hydrate(command.Name),
            DisplayName.Hydrate(command.DisplayName),
            Description.Hydrate(command.Description));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. addedOrganization]);
    }
}
