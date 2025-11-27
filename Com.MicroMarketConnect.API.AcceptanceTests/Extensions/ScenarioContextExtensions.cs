using Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

namespace Reqnroll;

internal static class ScenarioContextExtensions
{
    private const string HttpError = "HttpError";

    internal static void SetError(this ScenarioContext context, HttpTestServerException exception)
    {
        context.Set(exception, HttpError);
    }

    internal static bool TryGetError(this ScenarioContext context, out HttpTestServerException error)
    {
        return context.TryGetValue(HttpError, out error);
    }

    internal static void RemoveError(this ScenarioContext context)
    {
        context.Remove(HttpError);
    }
}
