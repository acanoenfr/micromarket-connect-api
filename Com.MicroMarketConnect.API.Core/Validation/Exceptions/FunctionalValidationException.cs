using FluentResults;

namespace Com.MicroMarketConnect.API.Core.Validation.Exceptions;

[Serializable]
public class FunctionalValidationException : Exception
{
    public Error Error { get; }

    public FunctionalValidationException(Error error)
    {
        Error = error;
    }

    public FunctionalValidationException(string message, Error error)
        : base(message)
    {
        Error = error;
    }

    public FunctionalValidationException(string message, Exception innerException, Error error)
        : base(message, innerException)
    {
        Error = error;
    }
}
