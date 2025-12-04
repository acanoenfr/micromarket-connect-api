using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;

public enum MemberRoleEnum
{
    [EnumMember(Value = "Organization.Owner")]
    [EnumDataType(typeof(string))]
    Owner,

    [EnumMember(Value = "Organization.Manager")]
    [EnumDataType(typeof(string))]
    Manager,

    [EnumMember(Value = "Organization.Member")]
    [EnumDataType(typeof(string))]
    Member
}
