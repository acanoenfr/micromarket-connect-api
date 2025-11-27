using Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Retrievers;
using Reqnroll;
using Reqnroll.Assist;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations;

[Binding]
internal class AssistHooks
{
    [BeforeTestRun]
    public static void RegisterValueRetrievers()
    {
        Service.Instance.ValueRetrievers.Register(new DateOnlyValueRetriever("yyyy-MM-dd"));
        Service.Instance.ValueRetrievers.Register(new NullableDateOnlyValueRetriever("yyyy-MM-dd"));
    }
}
