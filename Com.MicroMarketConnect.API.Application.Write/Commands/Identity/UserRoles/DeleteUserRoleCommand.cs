using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.UserRoles;

public record DeleteUserRoleCommand(
    Guid UserId,
    string RoleName) : IEventDrivenCommand;

public class DeleteUserRoleCommandHandler : CommandHandler<DeleteUserRoleCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(DeleteUserRoleCommand command)
    {
        var userRoleDeleted = UserRole.Delete(
            RowId.Hydrate(command.UserId),
            RoleName.Hydrate(command.RoleName));

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. userRoleDeleted]);
    }
}
