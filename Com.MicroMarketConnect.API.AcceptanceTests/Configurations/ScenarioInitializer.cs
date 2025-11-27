using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

[Binding]
internal class ScenarioInitializer(ScenarioContext context) : IDisposable
{
    internal const string ExceptionHandleKeyContext = "ExceptionHandle";
    private const string ErrorHandlingTagKey = "ErrorHandling";
    private TestsServer? _server;

    private MicroMarketConnectDbContext DbContext => _server!.ServiceProvider.GetRequiredService<MicroMarketConnectDbContext>();

    [BeforeScenario]
    public async Task Init()
    {
        _server = new TestsServer(context);
        context.Set(_server);

        ITestsClient client = _server.ServiceProvider.GetRequiredService<ITestsClient>();
        await client.Initialize();
        await client.InitializeStaff();
    }

    [AfterStep]
    public void ThrowNotHandledErrors()
    {
        // Ensure other DbContext instance changes are reflected in current DbContext, which is done by forcing ChangeTracker to be cleared
        DbContext.ChangeTracker.Clear();

        if (!context.ScenarioInfo.Tags.Contains(ErrorHandlingTagKey))
        {
            ThrowExceptionIfNotHandled();
        }
    }

    [AfterScenario]
    public void Clean()
    {
        _server?.ForceStop();
        _server?.Dispose();
        ThrowExceptionIfNotHandled();
    }

    private void ThrowExceptionIfNotHandled()
    {
        if (context.TryGetError(out HttpTestServerException error))
        {
            throw error;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _server?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
