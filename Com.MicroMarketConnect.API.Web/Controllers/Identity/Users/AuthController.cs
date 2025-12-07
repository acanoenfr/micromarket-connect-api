using Asp.Versioning;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.User.Events;
using Com.MicroMarketConnect.API.Infrastructure.Orchestration;
using Com.MicroMarketConnect.API.Web.Builders;
using Com.MicroMarketConnect.API.Web.ViewModels.Identity.Auth;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Com.MicroMarketConnect.API.Web.Controllers.Identity;

[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[ApiController]
public class AuthController(WebDispatcher dispatcher) : ControllerBase
{
    [HttpPost("sign_in")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(UserSignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserSignInResponse>> SignIn([FromBody] UserSignInRequest request)
    {
        var result = await dispatcher.Dispatch(request.ToCommand());

        return result.ToActionResult(
            events =>
            {
                var @event = events.OfType<UserLoggedEvent>().First();

                return Ok(new UserSignInResponse(@event.Token.Value));
            },
            errors => errors.FirstOrDefault() switch
            {
                NotFound _ => NotFound(),
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    [HttpPost("sign_up")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(UserSignUpResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserSignUpResponse>> SignUp([FromBody] UserSignUpRequest request)
    {
        var result = await dispatcher.Dispatch(request.ToCommand());

        return result.ToActionResult(
            events =>
            {
                var @event = events.OfType<UserAddedEvent>().First();
                var builder = HateOasUriBuilder
                    .CreateBuilder()
                    .AppendPathWithoutVersion(HttpContext.Request)
                    .AppendSegmentValue(@event.Id.Value.ToString());

                return Created(builder.GenerateUriString(), new UserSignUpResponse(
                    @event.Id.Value,
                    @event.DisplayName.Value,
                    @event.Email.Value));
            },
            errors => errors.FirstOrDefault() switch
            {
                ValidationError { Code: var code } ve => code switch
                {
                    "ERROR_INVALID_DISPLAY_NAME_FIELD" => BadRequest(BuildProblemDetails(StatusCodes.Status400BadRequest, ve)),
                    "ERROR_INVALID_EMAIL_FIELD" => BadRequest(BuildProblemDetails(StatusCodes.Status400BadRequest, ve)),
                    "ERROR_EMAIL_ALREADY_EXISTS" => Conflict(BuildProblemDetails(StatusCodes.Status409Conflict, ve)),
                    _ => BadRequest(BuildProblemDetails(StatusCodes.Status400BadRequest, ve))
                },
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    public static ProblemDetails BuildProblemDetails(int statusCode, ValidationError err)
        => new()
        {
            Status = statusCode,
            Title = err.Message,
            Extensions = { ["code"] = err.Code }
        };
}
