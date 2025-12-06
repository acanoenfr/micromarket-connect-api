namespace Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;

public struct JwtClaims
{
    public const string Id = "id";
    public const string Email = "email";
    public const string EmailVerified = "email_verified";
    public const string PreferredUsername = "preferred_username";
    public const string Roles = "roles";
}
