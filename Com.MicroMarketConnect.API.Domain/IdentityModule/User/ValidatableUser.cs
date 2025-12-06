using Com.MicroMarketConnect.API.Core.Validation;
using FluentResults;

namespace Com.MicroMarketConnect.API.Domain.IdentityModule.User;

public static class UserErrorCodes
{
    public static readonly string ErrorInvalidDisplayNameField = "ERROR_INVALID_DISPLAY_NAME_FIELD";
    public static readonly string ErrorInvalidEmailField = "ERROR_INVALID_EMAIL_FIELD";
}

public sealed class ValidatableUser(
    string displayName,
    string email)
{
    public Result Validate()
    {
        if (string.IsNullOrWhiteSpace(displayName))
            return Result.Fail(Errors.ValidationError(UserErrorCodes.ErrorInvalidDisplayNameField, "Displayname field should be set."));

        if (string.IsNullOrWhiteSpace(email))
            return Result.Fail(Errors.ValidationError(UserErrorCodes.ErrorInvalidEmailField, "Email address field should be set."));

        return Result.Ok();
    }
}
