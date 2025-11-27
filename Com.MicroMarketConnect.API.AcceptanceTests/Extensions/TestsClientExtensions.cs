using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Clients;
using Com.MicroMarketConnect.API.AcceptanceTests.Extensions.Requests.ApiLatest;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Extensions;

internal static class TestsClientExtensions
{
    private const string LatestApiVersion = "v1";

    internal static TestsClientRequestsLatest Latest(this ITestsClient client) => new(client, LatestApiVersion);
}
