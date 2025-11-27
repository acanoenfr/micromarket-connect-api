using Com.MicroMarketConnect.API.AcceptanceTests.Configurations;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Reqnroll;
using System.Net;
using System.Text.Json;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Features;

[Binding]
internal class CommonSteps : StepsBase
{
    public CommonSteps(ScenarioContext context) : base(context)
    {
    }

    [Given(@"I am connected with the role {string}")]
    public void GivenIAmConnected(string role)
    {
        Client.AuthenticateUser("TestUserId", [role]);
    }

    [Given(@"I am not connected")]
    public void GivenIAmNotConnected()
    {
    }

    [Then(@"a success is returned with an HTTP status {int}")]
    public void ThenASuccessIsReturnedWithAnHttpStatus(int httpCode)
    {
        Response.StatusCode.Should().Be((HttpStatusCode)httpCode);
    }

    [Then(@"an error is thrown with an HTTP status {int}")]
    public void ThenAnErrorIsThrownWithAnHttpStatus(int httpErrorCode)
    {
        Context.TryGetError(out HttpTestServerException error);

        error.Should().NotBeNull();
        error.StatusCode.Should().Be((HttpStatusCode)httpErrorCode);

        Context.RemoveError();
    }

    [Then("the error contains the code {string}")]
    public async Task ThenTheErrorContainsTheCode(string error)
    {
        var content = await Response.Content.ReadAsStringAsync();
        var details = JsonSerializer.Deserialize<ProblemDetails>(content);

        details.Should().NotBeNull();
        details.Extensions.Should().ContainKey("code")
            .WhoseValue.As<JsonElement>().GetString().Should().Be(error);
    }
}
