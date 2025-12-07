using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.OrganizationMembers;

public class AddOrganizationMemberRequest
{
    public Guid UserId { get; set; }
    public required string Role { get; set; }

    public AddOrganizationMemberCommand ToCommand(Guid OrganizationId)
        => new(OrganizationId, UserId, Role);
}
