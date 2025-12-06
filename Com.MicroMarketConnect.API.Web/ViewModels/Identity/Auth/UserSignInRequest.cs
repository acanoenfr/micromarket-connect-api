using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Users;

namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Auth;

public class UserSignInRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }

    public LoginCommand ToCommand()
        => new(Username, Password);
}
