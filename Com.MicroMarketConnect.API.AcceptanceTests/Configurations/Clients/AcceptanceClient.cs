using Reqnroll;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;

internal class AcceptanceClient(
    HttpClient httpClient,
    ScenarioContext context) : TestClientBase(context, httpClient), ITestsClient
{
    public Task InitializeStaff()
    {
        return Task.CompletedTask;
    }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public void AuthenticateUser(string userId, string[] roles)
    {
        Client.DefaultRequestHeaders.Add(TestAuthHandler.TestingHeaderName, TestAuthHandler.TestingHeaderValue);

        if (userId is not null)
        {
            Client.DefaultRequestHeaders.Add(TestAuthHandler.TestingUserIdHeaderName, userId);
        }

        if (roles is not null)
        {
            Client.DefaultRequestHeaders.Add(TestAuthHandler.TestingRolesHeaderName, roles);
        }
    }
}
