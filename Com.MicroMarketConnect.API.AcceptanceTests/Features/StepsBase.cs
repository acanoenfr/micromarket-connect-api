using Com.MicroMarketConnect.API.AcceptanceTests.Configurations;
using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;
using Com.MicroMarketConnect.API.AcceptanceTests.Spies;
using Com.MicroMarketConnect.API.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using System.Text.Json;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Features;

internal abstract class StepsBase
{
    protected readonly ScenarioContext Context;
    protected readonly MicroMarketConnectDbContext DbContext;

    protected HttpResponseMessage Response
    {
        get => Context.Get<HttpResponseMessage>();
        set => Context.Set(value);
    }

    protected StepsBase(ScenarioContext context)
    {
        Context = context;
        Client = TestServer.ServiceProvider.GetRequiredService<ITestsClient>();
        DbContext = GetService<MicroMarketConnectDbContext>();
    }

    private IServiceProvider ServiceProvider => TestServer.ServiceProvider;
    protected TestsServer TestServer => Context.Get<TestsServer>();
    protected ITestsClient Client { get; }

    protected void HandleException(Exception ex)
    {
        Context.Set(ex, ScenarioInitializer.ExceptionHandleKeyContext);
    }

    protected void SetExceptionHandled()
    {
        Context.Remove(ScenarioInitializer.ExceptionHandleKeyContext);
    }

    protected void DefineGuidProviderValue(Guid guid)
    {
        DefineGuidProviderValue(new List<Guid> { guid });
    }

    protected void DefineGuidProviderValue(List<Guid> guidList)
    {
        GetService<SpyGuidProvider>().Guids = guidList;
    }

    protected void DefineDateProviderValue(DateTimeOffset? date)
    {
        if (date.HasValue)
            DefineDateProviderValue(new List<DateTimeOffset> { date.Value });
    }

    protected void DefineDateProviderValue(List<DateTimeOffset> dateList)
    {
        GetService<SpyDateProvider>().Dates = dateList;
    }

    protected void DefineUserProviderValue(Guid id, string email, string[] roles)
    {
        GetService<SpyUserProvider>().Id = id;
        GetService<SpyUserProvider>().Email = email;
        GetService<SpyUserProvider>().Roles = roles;
    }

    protected T GetService<T>()
    where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    protected async Task<T> DeserializeResponseContent<T>()
    {
        if (!Response.IsSuccessStatusCode)
            return default!;

        var json = await Response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
    }

    public Exception LastException
    {
        get
        {
            if (Context.TryGetValue(ScenarioInitializer.ExceptionHandleKeyContext, out Exception value))
            {
                return value;
            }

            throw new InvalidOperationException("No exception was thrown.");
        }
    }
}
