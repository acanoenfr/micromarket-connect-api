using Com.MicroMarketConnect.API.AcceptanceTests.Extensions;
using Reqnroll;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Features.HealthChecks;

[Binding, Scope(Feature = "Case of getting health checks")]
internal class CaseOfGettingSteps(ScenarioContext context) : StepsBase(context)
{
    [When(@"I request liveness health check")]
    public async Task WhenIRequestLivenessHealthCheck()
    {
        Response = await Client
            .Latest()
            .HealthRequests()
            .GetLiveness();
    }

    [When(@"I request readiness health check")]
    public async Task WhenIRequestReadinessHealthCheck()
    {
        Response = await Client
            .Latest()
            .HealthRequests()
            .GetReadiness();
    }
}
