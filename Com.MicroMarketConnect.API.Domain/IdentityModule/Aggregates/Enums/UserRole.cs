using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;

public enum UserRole
{
    #region Platform roles

    [EnumMember(Value = "Platform.Admin")]
    [EnumDataType(typeof(string))]
    PlatformAdmin,

    [EnumMember(Value = "Platform.Moderator")]
    [EnumDataType(typeof(string))]
    PlatformModerator,

    [EnumMember(Value = "Platform.User")]
    [EnumDataType(typeof(string))]
    PlatformUser

    #endregion
}
