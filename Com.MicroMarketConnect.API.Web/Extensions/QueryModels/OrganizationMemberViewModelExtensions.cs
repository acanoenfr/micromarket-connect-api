using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.OrganizationMembers;
using Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;

namespace Com.MicroMarketConnect.API.Web.Extensions.QueryModels;

public static class OrganizationMemberViewModelExtensions
{
    public static IReadOnlyCollection<OrganizationMemberResponse> ToViewModel(this IReadOnlyCollection<OrganizationMemberQueryModel> model)
        => model.Select(ToViewModel).ToList().AsReadOnly();

    public static OrganizationMemberResponse ToViewModel(this OrganizationMemberQueryModel model)
        => new(
            model.Id,
            model.DisplayName,
            model.Email,
            model.Role);
}
