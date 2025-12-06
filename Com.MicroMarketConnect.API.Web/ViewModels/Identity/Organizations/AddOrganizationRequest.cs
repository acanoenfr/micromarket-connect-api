using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;

public class AddOrganizationRequest
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }

    public AddOrganizationCommand ToCommand()
        => new(Name, DisplayName, Description);
}
