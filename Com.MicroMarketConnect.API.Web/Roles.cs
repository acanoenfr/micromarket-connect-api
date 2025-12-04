using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Infrastructure.Extensions;

namespace Com.MicroMarketConnect.API.Web;

public static class Roles
{
    #region Platform roles

    public static readonly string PlatformUser = UserRoleEnum.PlatformUser.GetValue();
    public static readonly string PlatformModerator = UserRoleEnum.PlatformModerator.GetValue();
    public static readonly string PlatformAdmin = UserRoleEnum.PlatformAdmin.GetValue();

    #endregion
}
