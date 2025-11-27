using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Extensions.Requests.ApiLatest;

internal class TestsClientRequestsLatest
{
    private readonly ITestsClient _client;
    private readonly string _apiVersion;

    public TestsClientRequestsLatest(ITestsClient client, string apiVersion)
    {
        _client = client;
        _apiVersion = apiVersion;
    }

    public HealthRequestsLatest HealthRequests() => new(_client, _apiVersion);
}
