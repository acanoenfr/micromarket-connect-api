using Microsoft.AspNetCore.Mvc;

namespace FluentResults;

public static class ResultExtentions
{
    public static ActionResult ToActionResult<T>(this Result<T> result, Func<T, ActionResult> success, Func<IEnumerable<IError>, ActionResult> failure)
    {
        if (result.IsSuccess)
            return success.Invoke(result.Value);

        return failure.Invoke(result.Errors);
    }

    public static ActionResult ToActionResult(this Result result, Func<ActionResult> success, Func<IEnumerable<IError>, ActionResult> failure)
    {
        if (result.IsSuccess)
            return success.Invoke();

        return failure.Invoke(result.Errors);
    }
}
