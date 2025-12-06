using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;

namespace Com.MicroMarketConnect.API.Web.ViewModels.Identity.OrganizationMembers;

public class UpdateOrganizationMemberRequest
{
    public required string Role { get; set; }

    public UpdateOrganizationMemberCommand ToCommand(Guid OrganizationId, Guid UserId)
        => new(OrganizationId, UserId, Role);
}
