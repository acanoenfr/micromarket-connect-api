using Com.MicroMarketConnect.API.Domain.SharedModule.Enums;

namespace Com.MicroMarketConnect.API.Web;

public static class Roles
{
    #region Platform roles

    public static readonly string PlatformUser = UserRole.PlatformUser.ToString();
    public static readonly string PlatformModerator = UserRole.PlatformModerator.ToString();
    public static readonly string PlatformAdmin = UserRole.PlatformAdmin.ToString();

    #endregion
}
