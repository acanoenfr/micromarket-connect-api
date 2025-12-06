using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;

namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;

public class UpdateOrganizationRequest
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }

    public UpdateOrganizationCommand ToCommand(Guid Id)
        => new(Id, Name, DisplayName, Description);
}
