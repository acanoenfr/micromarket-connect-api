using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Users;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;

namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Auth;

public class UserSignUpRequest
{
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    public required string PlainPassword { get; set; }

    public AddUserCommand ToCommand()
        => new(DisplayName, Email, PlainPassword, [UserRoleClaims.PlatformUser]);
}
