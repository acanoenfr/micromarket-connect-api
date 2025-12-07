using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;
using Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;

namespace Com.MicroMarketConnect.API.Web.Extensions.QueryModels;

public static class OrganizationQueryModelExtensions
{
    public static OrganizationResponse ToViewModel(this OrganizationQueryModel model)
        => new(
            model.Id,
            model.Name,
            model.DisplayName,
            model.Description,
            model.Members.ToViewModel());

    public static IReadOnlyCollection<OrganizationMemberResponse> ToViewModel(this IReadOnlyCollection<OrganizationMember> model)
        => model.Select(ToViewModel).ToList().AsReadOnly();

    public static OrganizationMemberResponse ToViewModel(this OrganizationMember model)
        => new(
            model.Id,
            model.DisplayName,
            model.Email,
            model.Role);
}
