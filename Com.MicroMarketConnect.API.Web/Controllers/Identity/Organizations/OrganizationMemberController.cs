using Asp.Versioning;
using Com.MicroMarketConnect.API.Application.Write.Commands.Identity.OrganizationMembers;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using Com.MicroMarketConnect.API.Infrastructure.Orchestration;
using Com.MicroMarketConnect.API.Web.ViewModels.Identity.OrganizationMembers;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.MicroMarketConnect.API.Web.Controllers.Identity.Organizations;

[Route("api/v{version:apiVersion}/organizations")]
[ApiVersion("1.0")]
[ApiController]
public class OrganizationMemberController(WebDispatcher dispatcher) : ControllerBase
{
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
}
