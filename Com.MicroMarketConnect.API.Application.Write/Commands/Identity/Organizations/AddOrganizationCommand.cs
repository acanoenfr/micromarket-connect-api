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

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

public record AddOrganizationCommand(
    string Name,
    string DisplayName,
    string? Description) : IEventDrivenCommand;

public class AddOrganizationCommandHandler(
    IOrganizationCommandRepository repository,
    IUserProvider userProvider,
    IGuidProvider guidProvider) : CommandHandler<AddOrganizationCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(AddOrganizationCommand command)
    {
        var organizationExists = await repository.Exists(command.Name);
        if (organizationExists is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors);
        if (organizationExists.Value)
            return Result.Fail(Errors.Conflict);

        var id = RowId.Hydrate(guidProvider.NewGuid());
        var userId = RowId.Hydrate(userProvider.GetId());

        var addedOrganization = Organization.CreateWithOwner(
            id,
            Name.Hydrate(command.Name),
            DisplayName.Hydrate(command.DisplayName),
            Description.Hydrate(command.Description),
            userId,
            RoleName.Hydrate(OrganizationRoleClaims.Owner));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. addedOrganization]);
    }
}
