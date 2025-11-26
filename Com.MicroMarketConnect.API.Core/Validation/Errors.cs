using FluentResults;

namespace Com.MicroMarketConnect.API.Core.Validation;

public static class Errors
{
    public static Generic Generic { get; } = new Generic();
    public static NotFound NotFound { get; } = new NotFound();
    public static Conflict Conflict { get; } = new Conflict();
    public static BadRequest BadRequest { get; } = new BadRequest();

    public static ValidationError ValidationError(string code, string message) => new(code, message);
    public static Error ExceptionalError(Exception e) => new ExceptionalError(e);
}

public class ValidationError : Error
{
    public string Code { get; set; }

    public ValidationError(string code, string message) : base(message)
    {
        Code = code;
    }
}

public class Generic : Error { }
public class NotFound : Error { }
public class Conflict : Error { }
public class BadRequest : Error { }
