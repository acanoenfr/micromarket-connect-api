using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Extensions.Requests.ApiLatest;

internal class HealthRequestsLatest
{
    private readonly ITestsClient _client;
    private readonly string _basePath;

    public HealthRequestsLatest(ITestsClient client, string apiVersion)
    {
        _client = client;
        _basePath = "/health";
    }

    public Task<HttpResponseMessage> GetLiveness()
        => _client.Get(_basePath);

    public Task<HttpResponseMessage> GetReadiness()
        => _client.Get($"{_basePath}/ready");
}
