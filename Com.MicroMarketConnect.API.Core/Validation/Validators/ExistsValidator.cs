using FluentResults;

namespace Com.MicroMarketConnect.API.Core.Validation.Validators;

public class ExistsValidator<T> : IValidator<T>
{
    private readonly T? _objToValidate;

    private ExistsValidator(T? objToValidate)
    {
        _objToValidate = objToValidate;
    }

    public static ExistsValidator<T> Create(T? objToValidate)
    {
        return new ExistsValidator<T>(objToValidate);
    }

    public Result<T> Validate()
    {
        if (_objToValidate == null)
            return Result.Fail(Errors.NotFound);

        return Result.Ok(_objToValidate);
    }
}
