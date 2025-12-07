using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

public record UpdateOrganizationCommand(
    Guid Id,
    string Name,
    string DisplayName,
    string Description) : IEventDrivenCommand;

public class UpdateOrganizationCommandHandler(
    IOrganizationCommandRepository repository,
    IUserProvider userProvider) : CommandHandler<UpdateOrganizationCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(UpdateOrganizationCommand command)
    {
        var isGrantAccess = await repository.IsGrantAccess(command.Id, OrganizationRoleClaims.Owner, userProvider.GetId());
        if (isGrantAccess is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors.FirstOrDefault());
        if (!isGrantAccess.Value)
            return Result.Fail(Errors.Forbidden);

        var updatedOrganization = Organization.Update(
            RowId.Hydrate(command.Id),
            Name.Hydrate(command.Name),
            DisplayName.Hydrate(command.DisplayName),
            Description.Hydrate(command.Description));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. updatedOrganization]);
    }
}
