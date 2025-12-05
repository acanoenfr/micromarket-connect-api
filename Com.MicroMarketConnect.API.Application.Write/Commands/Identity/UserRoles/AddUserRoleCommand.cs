using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.UserRoles;

public record AddUserRoleCommand(
    Guid UserId,
    string RoleName) : IEventDrivenCommand;

public class AddUserRoleCommandHandler : CommandHandler<AddUserRoleCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(AddUserRoleCommand command)
    {
        var userRoleAdded = UserRole.Create(
            RowId.Hydrate(command.UserId),
            RoleName.Hydrate(command.RoleName));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. userRoleAdded]);
    }
}
