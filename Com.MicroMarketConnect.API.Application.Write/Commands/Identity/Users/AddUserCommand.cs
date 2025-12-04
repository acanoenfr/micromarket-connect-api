using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core;
using Com.MicroMarketConnect.API.Core.Extensions;
using Com.MicroMarketConnect.API.Core.Orchestration;
using Com.MicroMarketConnect.API.Core.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User;
using Com.MicroMarketConnect.API.Domain.SharedModule.Aggregates;
using FluentResults;

namespace Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Users;

public record AddUserCommand(
    string DisplayName,
    string Email,
    string PlainPassword,
    UserRoleEnum[] UserRoles) : IEventDrivenCommand;

public class AddUserCommandHandler(
    IUserCommandRepository repository,
    IGuidProvider guidProvider,
    IHasherProvider hasherProvider) : CommandHandler<AddUserCommand>
{
    protected override async Task<Result<IReadOnlyCollection<IDomainEvent>>> Handle(AddUserCommand command)
    {
        var userExists = await repository.Exists(command.Email);
        if (userExists is { IsFailed: true, Errors: var errors })
            return Result.Fail(errors);
        if (userExists.Value)
            return Result.Fail(Errors.ValidationError("ERROR_EMAIL_ALREADY_EXISTS", "Email is associated with an account"));

        var passwordHash = hasherProvider.Hash(command.PlainPassword);

        var validUser = BuildValidatableUser(command.DisplayName, command.Email).Validate();
        if (validUser is { IsFailed: true, Errors: var validationErrors })
            return Result.Fail(validationErrors);

        var roleNames = command.UserRoles
            .ToList()
            .Select(r => RoleName.Hydrate(r.GetValue()));

        var addedUser = User.Create(
            RowId.Hydrate(guidProvider.NewGuid()),
            DisplayName.Hydrate(command.DisplayName),
            EmailAddress.Hydrate(command.Email),
            Domain.IdentityModule.Aggregates.PasswordHash.Hydrate(passwordHash.Hash),
            PasswordSalt.Hydrate(passwordHash.Salt),
            roleNames);

        return Result.Ok<IReadOnlyCollection<IDomainEvent>>([.. addedUser]);
    }

    private ValidatableUser BuildValidatableUser(string displayName, string email)
        => new(displayName, email);
}
