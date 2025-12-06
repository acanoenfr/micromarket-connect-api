using Asp.Versioning;
using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.OrganizationMembers;
using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;
using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;
using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.Organizations;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Organization.Events;
using Com.MicroMarketConnect.API.Infrastructure.Orchestration;
using Com.MicroMarketConnect.API.Web.Builders;
using Com.MicroMarketConnect.API.Web.Extensions.QueryModels;
using Com.MicroMarketConnect.API.Web.ViewModels.Identity.OrganizationMembers;
using Com.MicroMarketConnect.API.Web.ViewModels.Identity.Organizations;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.MicroMarketConnect.API.Web.Controllers.Identity;

[Route("api/v{version:apiVersion}/organizations")]
[ApiVersion("1.0")]
[ApiController]
public class OrganizationController(WebDispatcher dispatcher) : ControllerBase
{
    [HttpGet("{name}")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrganizationResponse>> GetOrganization(
        [FromRoute(Name = "name")] string name)
    {
        var result = await dispatcher.Dispatch(new GetOrganizationQuery(name));

        return result.ToActionResult(
            v => Ok(v.ToViewModel()),
            err => err.FirstOrDefault() switch
            {
                _ => StatusCode(500, err.FirstOrDefault())
            });
    }

    [HttpGet("{name}/members")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IReadOnlyCollection<OrganizationMemberResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyCollection<OrganizationMemberResponse>>> GetOrganizationMembers(
        [FromRoute(Name = "name")] string name)
    {
        var result = await dispatcher.Dispatch(new GetOrganizationMemberQuery(name));

        return result.ToActionResult(
            v => Ok(v.ToViewModel()),
            err => err.FirstOrDefault() switch
            {
                _ => StatusCode(500, err.FirstOrDefault())
            });
    }

    #region Organization actions

    [HttpPost]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> AddOrganization(
        [FromBody] AddOrganizationRequest request)
    {
        var result = await dispatcher.Dispatch(request.ToCommand());

        return result.ToActionResult(
            events =>
            {
                var @event = events.OfType<OrganizationAddedEvent>().First();
                var builder = HateOasUriBuilder
                    .CreateBuilder()
                    .AppendPathWithoutVersion(HttpContext.Request)
                    .AppendSegmentValue(@event.Id.Value.ToString());

                return Created(builder.GenerateUriString(), @event.Id.Value);
            },
            errors => errors.FirstOrDefault() switch
            {
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateOrganization(
        [FromRoute(Name = "id")] Guid id,
        [FromBody] UpdateOrganizationRequest request)
    {
        var result = await dispatcher.Dispatch(request.ToCommand(id));

        return result.ToActionResult(
            events => NoContent(),
            errors => errors.FirstOrDefault() switch
            {
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteOrganization(
        [FromRoute(Name = "id")] Guid id)
    {
        var result = await dispatcher.Dispatch(new DeleteOrganizationCommand(id));

        return result.ToActionResult(
            events => NoContent(),
            errors => errors.FirstOrDefault() switch
            {
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    #endregion

    #region Member actions

    [HttpPost("{id}/members")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddOrganizationMember(
        [FromRoute(Name = "id")] Guid id,
        [FromBody] AddOrganizationMemberRequest request)
    {
        var result = await dispatcher.Dispatch(request.ToCommand(id));

        return result.ToActionResult(
            events => NoContent(),
            errors => errors.FirstOrDefault() switch
            {
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    [HttpPut("{id}/members/{userId}")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateOrganizationMember(
        [FromRoute(Name = "id")] Guid id,
        [FromRoute(Name = "userId")] Guid userId,
        [FromBody] UpdateOrganizationMemberRequest request)
    {
        var result = await dispatcher.Dispatch(request.ToCommand(id, userId));

        return result.ToActionResult(
            events => NoContent(),
            errors => errors.FirstOrDefault() switch
            {
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    [HttpDelete("{id}/members/{userId}")]
    [Authorize(Roles = UserRoleClaims.PlatformUser)]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteOrganizationMember(
        [FromRoute(Name = "id")] Guid id,
        [FromRoute(Name = "userId")] Guid userId)
    {
        var result = await dispatcher.Dispatch(new DeleteOrganizationMemberCommand(id, userId));

        return result.ToActionResult(
            events => NoContent(),
            errors => errors.FirstOrDefault() switch
            {
                _ => StatusCode(500, errors.FirstOrDefault())
            });
    }

    #endregion
}
