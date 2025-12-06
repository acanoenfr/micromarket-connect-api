namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Auth;

public record UserSignUpResponse(
    Guid Id,
    string DisplayName,
    string Email);
