namespace Com.MicroMarketConnect.API.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string ToUpperFirstCharacter(this string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }

    public static string ToUpperInvariantFirstCharacter(this string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            return input;

        return char.ToUpperInvariant(input[0]) + input.Substring(1);
    }

    public static string ToLowerFirstCharacter(this string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static string ToLowerInvariantFirstCharacter(this string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            return input;

        return char.ToLowerInvariant(input[0]) + input.Substring(1);
    }
}
