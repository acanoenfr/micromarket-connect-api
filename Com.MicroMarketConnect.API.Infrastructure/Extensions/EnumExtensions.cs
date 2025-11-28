using System.Reflection;
using System.Runtime.Serialization;

namespace Com.MicroMarketConnect.API.Infrastructure.Extensions;

public static class EnumExtensions
{
    public static string GetValue(this Enum enumValue)
    {
        var memberInfo = enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault();

        if (memberInfo == null)
            return enumValue.ToString();

        var enumMemberAttr = memberInfo
            .GetCustomAttribute<EnumMemberAttribute>();

        return enumMemberAttr?.Value ?? enumValue.ToString();
    }

    public static TEnum FromValue<TEnum>(this string value) where TEnum : struct, Enum
    {
        foreach (var field in typeof(TEnum).GetFields())
        {
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
            if (attribute != null && attribute.Value == value)
                return (TEnum)field.GetValue(value)!;

            if (field.Name == value)
                return (TEnum)field.GetValue(null)!;
        }

        throw new ArgumentException($"Unknown value '{value}' for enum '{typeof(TEnum).Name}'.");
    }
}
