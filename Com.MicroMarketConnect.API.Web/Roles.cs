using Com.MicroMarketConnect.API.Domain.SharedModule.Enums;
using Com.MicroMarketConnect.API.Infrastructure.Extensions;

namespace Com.MicroMarketConnect.API.Web;

public static class Roles
{
    #region Platform roles

    public static readonly string PlatformUser = UserRole.PlatformUser.GetValue();
    public static readonly string PlatformModerator = UserRole.PlatformModerator.GetValue();
    public static readonly string PlatformAdmin = UserRole.PlatformAdmin.GetValue();

    #endregion
}
