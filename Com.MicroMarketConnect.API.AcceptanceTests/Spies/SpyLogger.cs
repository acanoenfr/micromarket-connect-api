using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Spies;

internal class SpyLogger<T> : ILogger<T>
{
    private readonly List<(LogLevel logLevel, string message, Exception? exception)> _logs = new();
    public IReadOnlyList<(LogLevel logLevel, string message, Exception? exception)> Logs => _logs;

    public IDisposable BeginScope<TState>(TState state) => new DummyScope();

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        => _logs.Add((logLevel, formatter(state, exception), exception));

    private sealed record DummyScope : IDisposable
    {
        public void Dispose()
        {
            // Dummy
        }
    }
}
