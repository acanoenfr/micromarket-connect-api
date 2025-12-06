namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;

public record OrganizationResponse(
    Guid Id,
    string Name,
    string DisplayName,
    string? Description);
