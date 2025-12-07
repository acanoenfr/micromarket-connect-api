using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Users;

public record LoginCommand(string Username, string Password) : IEventDrivenCommand;

public class LoginCommandHandler(IUserCommandRepository repository) : CommandHandler<LoginCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(LoginCommand command)
    {
        var result = await repository.Login(command.Username, command.Password);
        if (result is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors.FirstOrDefault());

        var token = Token.Hydrate(result.Value);
        var list = new List<IDomainEvent>()
        {
            new UserLoggedEvent(token)
        };

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>(list.AsReadOnly());
    }
}
