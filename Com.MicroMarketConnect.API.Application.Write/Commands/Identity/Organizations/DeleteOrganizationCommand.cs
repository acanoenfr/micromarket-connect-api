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

public record DeleteOrganizationCommand(Guid Id) : IEventDrivenCommand;

public class DeleteOrganizationCommandHandler(
    IOrganizationCommandRepository repository,
    IUserProvider userProvider): CommandHandler<DeleteOrganizationCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(DeleteOrganizationCommand command)
    {
        var isGrantAccess = await repository.IsGrantAccess(command.Id, OrganizationRoleClaims.Owner, userProvider.GetId());
        if (isGrantAccess is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors.FirstOrDefault());
        if (!isGrantAccess.Value)
            return Result.Fail(Errors.Forbidden);

        var deletedOrganization = Organization.Delete(
            RowId.Hydrate(command.Id));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. deletedOrganization]);
    }
}
