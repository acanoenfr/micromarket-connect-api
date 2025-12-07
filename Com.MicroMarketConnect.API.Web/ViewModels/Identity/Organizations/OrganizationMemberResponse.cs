namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;

public record OrganizationMemberResponse(
    Guid Id,
    string DisplayName,
    string Email,
    string Role);
