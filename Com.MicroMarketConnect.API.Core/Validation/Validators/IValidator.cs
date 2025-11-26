using FluentResults;

namespace Com.MicroMarketConnect.API.Core.Validation.Validators;

public interface IValidator<T>
{
    Result<T> Validate();
}
