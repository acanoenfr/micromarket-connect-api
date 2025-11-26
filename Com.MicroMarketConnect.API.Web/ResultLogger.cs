using FluentResults;

namespace Com.MicroMarketConnect.API.Web;

public class ResultLogger(ILogger<ResultLogger> logger) : IResultLogger
{
    public void Log(string context, string content, ResultBase result, LogLevel logLevel)
        => logger.Log(logLevel, "Result: {Reasons} {Content} <{Context}>", result.Reasons.Select(r => r.Message), content, context);

    public void Log<TContext>(string content, ResultBase result, LogLevel logLevel)
        => logger.Log(logLevel, "Result: {Reasons} {Content} <{Context}>", result.Reasons.Select(r => r.Message), content, typeof(TContext).FullName);
}
